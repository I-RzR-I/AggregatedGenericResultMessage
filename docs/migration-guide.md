# Migration guide — next major version (1.4.1.* -> 2.0.0.*)

This guide collects every source- or behavior-breaking change introduced in the
upcoming major release of `AggregatedGenericResultMessage`, plus the new APIs
that replace the obsolete ones. Each item shows a **before / after** snippet
so you can mechanically port your callers.

> **TL;DR**
> - All `ActionOn*` and `Func<Task<TResult>>`-taking `Function*` overloads are now `[Obsolete]`. Replace with `Tap` / `Match` / `*Async`.
> - `Result<T>.Instance` is now `[Obsolete]`. Replace with `Result<T>.Create()`.
> - Implicit `Exception → Result` / `Result<T>` operators **throw** on `null` instead of producing a silent empty failure.
> - `Result<T>(Exception)` ctor adds **one** `Exception` message instead of two.
> - `JoinErrors` now derives `IsSuccess` from the input collection (was a bug).
> - `Success<T>(T, params RelatedObjectModel[])` no longer adds a ghost `MessageType.None` message when `relatedObjects` is empty/null.
> - `ExceptionHelper.PreserveStackTrace` now returns `ExceptionDispatchInfo` instead of `void`.
> - `RelatedObjectModel.ToString()` is null-safe.
> - New combinators added: `Map`, `Bind`, `Match`, `Tap` (sync + async), `Validate` / `Ensure`, `FunctionExtensionsAsync.*Async`.

---

## 1. Compile-time obsoletions

These changes raise `CS0618` warnings. Builds with `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` will **fail**. Either suppress with `#pragma warning disable CS0618` or migrate to the replacements below.

### 1.1 `Result<T>.Instance` to `Result<T>.Create()`

`Instance` was misleading, every access already returned a *new* instance. The new factory makes intent explicit.

```csharp
// Before
var r = Result<Foo>.Instance;

// After
var r = Result<Foo>.Create();
```

### 1.2 `ActionOnSuccess` to `Tap`

```csharp
// Before
result.ActionOnSuccess(r => _logger.LogInformation("ok: {V}", r.Response));

// After
result.Tap(value => _logger.LogInformation("ok: {V}", value));
```

`Tap` returns the same instance for chaining and only invokes the action on success.

### 1.3 `ActionOnFailure` / `ActionOn` to `Match`

```csharp
// Before
result.ActionOn(
    onSuccess: r => _audit.Saved(r.Response),
    onFailure: r => _logger.LogError("failed: {Msg}", r.GetFirstMessage()));

// After (side-effect form returns the same IResult)
result.Match(
    onSuccess: r => _audit.Saved(((Result<Foo>)r).Response),
    onFailure: r => _logger.LogError("failed: {Msg}", r.GetFirstMessage()));

// After (fold-into-value form — the recommended pattern when you want a return value)
int httpStatus = result.Match(
    onSuccess: _ => 200,
    onFailure: _ => 500);
```

### 1.4 `FunctionOn*` / `ExecuteFunction` taking `Func<Task<TResult>>` to `*Async` equivalents

The seven sync-over-async overloads in `FunctionExtensions` are obsolete because awaiting a `Func<Task<T>>` from a synchronous extension is a possible deadlock. Use `RzR.ResultMessage.Extensions.Result.Functions.FunctionExtensionsAsync` instead.

```csharp
// Before- sync-over-async
var r = result.FunctionOnSuccess(async r => await _store.SaveAsync(r.Response));

// After
var r = await result.FunctionOnSuccessAsync(async r => await _store.SaveAsync(r.Response));
```

| Before                                              | After                                                         |
| --------------------------------------------------- | ------------------------------------------------------------- |
| `FunctionOnSuccess(Func<TResult, Task<TOut>>)`      | `FunctionOnSuccessAsync(Func<TResult, Task<TOut>>)`           |
| `FunctionOnFailure(Func<TResult, Task<TOut>>)`      | `FunctionOnFailureAsync(Func<TResult, Task<TOut>>)`           |
| `FunctionOn(...) ` 4 overloads with async delegates | `FunctionOnAsync(...)` 4 overloads                            |
| `ExecuteFunction(Func<TResult, Task<TOut>>)`        | `ExecuteFunctionAsync(Func<TResult, Task<TOut>>)`             |

---

## 2. Runtime behavior changes (silent- read carefully)

These do **not** raise warnings, but can change observable behavior.

### 2.1 Implicit `Exception -> Result` / `Result<T>` throws on `null`

```csharp
// Before
Result r = (Exception)null; // r.IsSuccess == false, r.Messages empty (silent)

// After
Result r = (Exception)null; // throws ArgumentNullException
```

If you previously relied on the silent fallback, guard at the call site:

```csharp
Exception ex = TryGetException();
Result r = ex is null ? Result.Failure() : ex;
```

### 2.2 `Result<T>(Exception)` ctor adds one message instead of two

```csharp
// Before
new Result<Foo>(ex).Messages.Count;  // 2  (empty info + trace)

// After
new Result<Foo>(ex).Messages.Count;  // 1  (single MessageType.Exception entry)
```

If your tests asserted `Count == 2`, change them to `Count == 1`.

### 2.3 `GetFirstMessage` / `GetFirstError` fall back to exception messages

When a result contains only exception messages (no plain `Info`/`Error` text), these helpers used to return an empty string. They now return the exception's message text.

```csharp
// Before
new Result<Foo>(new InvalidOperationException("boom")).GetFirstMessage(); // ""

// After
new Result<Foo>(new InvalidOperationException("boom")).GetFirstMessage(); // "boom"
```

### 2.4 `JoinErrors(IEnumerable<Result>)` derives `IsSuccess` from the collection

Previously the joined result inherited `IsSuccess` from the calling instance, which was a bug — joining a failed result with a list of successes incorrectly stayed failed (and vice versa).

```csharp
// Before
var joined = Result<int>.Success().JoinErrors(new[] { Result.Failure("e") });
joined.IsSuccess; // true (wrong-inherited from caller)

// After
var joined = Result<int>.Success().JoinErrors(new[] { Result.Failure("e") });
joined.IsSuccess; // false  (correctly: not all inputs are successful)
```

### 2.5 `Success<T>(T, params RelatedObjectModel[])` no longer adds a ghost message

```csharp
// Before
Result<Foo>.Success(foo).Messages.Count; // 1 (a stray MessageType.None entry)
Result<Foo>.Success(foo, related).Messages.Count; // 1 (the related-object message)

// After
Result<Foo>.Success(foo).Messages.Count; // 0
Result<Foo>.Success(foo, related).Messages.Count; // 1 (unchanged)
```

If you asserted `Messages.Count == 1` for a plain `Success(value)` call, update to `0`.

### 2.6 `ExceptionHelper.PreserveStackTrace` signature change

Rewritten on top of `ExceptionDispatchInfo`.

```csharp
// Before
public static void PreserveStackTrace(Exception ex);
ExceptionHelper.PreserveStackTrace(ex);
throw ex;

// After
public static ExceptionDispatchInfo PreserveStackTrace(Exception ex);
ExceptionHelper.PreserveStackTrace(ex).Throw();
```

### 2.7 `RelatedObjectModel.ToString()` is null-safe

Calling `ToString()` on a `RelatedObjectModel` with a `null` `InDataSourceNames` no longer throws `NullReferenceException`. Output format for the null case:

```
InCodeName: <name> <-> InDataSourceName:
```

If you parse `ToString()` output (not recommended), allow for a trailing empty segment.

---

## 3. New APIs (additive — no migration required, but recommended)

### 3.1 Synchronous monadic combinators

Available as instance methods on `Result<T>`:

```csharp
Result<int> mapped = source.Map(x => x.Length);
Result<Order> bound  = source.Bind(id => _repo.GetOrder(id));
int value  = source.Match(onSuccess: r => 200, onFailure: _ => 500);
Result<int> same   = source.Tap(v => _logger.LogInformation("v={V}", v));
```

### 3.2 Async combinators

Available as extension methods in `RzR.ResultMessage.Extensions.Result`. Each combinator is offered both on `Result<T>` and on `Task<Result<T>>`, with sync and async delegates, so a full pipeline composes fluently:

```csharp
using RzR.ResultMessage.Extensions.Result;

var dto = await _repo.GetOrderByIdAsync(id) // Task<Result<Order>>
    .MapAsync(o => o.ToSummary()) // sync projection
    .BindAsync(async s => await _enricher.EnrichAsync(s))
    .TapAsync(async s => await _audit.WriteAsync(s))
    .MatchAsync(
        onSuccess: s => OrderResponse.From(s),
        onFailure: r => OrderResponse.Error(r.GetFirstError()));
```

### 3.3 Validation aggregation: `Validate` / `Ensure`

| Method                          | Behavior                                                                                            |
| ------------------------------- | --------------------------------------------------------------------------------------------------- |
| `Validate(predicate, error)`    | Always evaluates; chain to **accumulate** every violation. Overloads for `(key, error)` and `MessageDataModel`. |
| `Ensure(predicate, error)`      | **Short-circuits** once `IsSuccess == false`, safe for guard chains where later predicates would NRE. |
| `ValidateAsync(...)`            | Async predicate, on both `Result<T>` and `Task<Result<T>>`.                                          |

```csharp
// Accumulating
var result = Result<Order>.Success(order)
    .Validate(o => o.Items.Count > 0, "items required")
    .Validate(o => o.Total > 0, "total > 0")
    .Validate(o => o.Customer != null, "customer required");

// Short-circuiting (later predicates skipped if any earlier one fails)
var safe = Result<User>.Success(user)
    .Ensure(u => u != null, "user required")
    .Ensure(u => !string.IsNullOrEmpty(u.Email), "email required")
    .Ensure(u => u.Email.Contains("@"), "email must be valid");
```

---

## 4. Backward-compatibility shims considered

The obsolete attributes still allow existing code to compile and run. There is **no compatibility shim** for the runtime behavior changes in §2 — they are intentional bug fixes. If you depended on the old behavior, you must update at the call site.

If you need to stay on the old behavior temporarily, pin the previous major version in your `PackageReference` while you migrate.
