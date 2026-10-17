# Migration guide — 3.0.x -> next major version (4.0.0): `Key` to `Code`

The message identifier is renamed from `Key` to `Code` in the object model, in method parameters and in every serialized payload. It holds the same thing as before: an error code, a localization key, or a culture id or name such as `en-US`.

> **TL;DR**
> - Use `.Code` instead of `.Key` on `MessageModel`, `IMessageModel`, `ResultErrorModel` and `IResultErrorModel`. `Key` stays as an `[Obsolete]` alias (`CS0618`).
> - Named `key:` arguments become `code:` and fail with `CS1739` until renamed. Positional calls and already-compiled callers keep working.
> - The wire format changes: JSON writes `"code"` / `"Code"`, XML writes `<Code>`. Old payloads still deserialize, but `Code` is `null`.
> - If you implement `IMessageModel` or `IResultErrorModel` yourself, add `Code` and recompile.
> - While old and new versions run side by side, codes can arrive as `null`. Do not use a code as the only security or flow gate.

---

## 1. `Key` to `Code` on the message models

`MessageModel`, `IMessageModel`, `ResultErrorModel` and `IResultErrorModel` expose `Code` instead of `Key`.

```csharp
// Before
string code = result.Messages.First().Key;
if (error.Key == "E404") { /* ... */ }

// After
string code = result.Messages.First().Code;
if (error.Code == "E404") { /* ... */ }
```

`Key` stays as an alias, so existing code still compiles:

- It forwards to `Code`. On `MessageModel` and `IMessageModel` you can read and write it. On `ResultErrorModel` and `IResultErrorModel` it is read-only.
- It carries `[Obsolete("Use Code instead. Key will be removed in a future major version.")]`, so every use raises `CS0618`. Builds with `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` fail until you migrate. To postpone, suppress the warning locally with `#pragma warning disable CS0618`.
- `[EditorBrowsable(EditorBrowsableState.Never)]` hides it from IntelliSense when you reference the package or the assembly. Project references still list it.
- No serializer writes it: XmlSerializer, DataContractSerializer, Newtonsoft.Json, and System.Text.Json on the netstandard builds. The exception is the .NET Framework builds, see [.NET Framework](#net-framework-net45-to-net48).
- Reflection-based tools (AutoMapper configuration validation, exporters, property scanners) see both `Code` and `Key` as public properties. Exclude `Key` in their configuration.

## 2. Named argument `key:` to `code:`

The parameter that carries the identifier is renamed from `key` to `code` on:

- `AddError`, `AddErrorConfirm`, `AddInfo`, `AddInfoConfirm`, `AddWarning`, `AddWarningConfirm`, `AddNotFound`, `AddAccessDenied`, `AddMessage` and `Validate`
- the `MessageModel` and `ResultErrorModel` constructors

```csharp
// Before
result.AddError(key: "E001", error: "Order not found");
result.AddMessage(key: "E002", message: "Invalid state", type: MessageType.Error);
result.Validate(o => o.Total > 0, key: "TOTAL", error: "total > 0");
var m = new MessageModel(key: "E003", message: "Timeout");

// After
result.AddError(code: "E001", error: "Order not found");
result.AddMessage(code: "E002", message: "Invalid state", type: MessageType.Error);
result.Validate(o => o.Total > 0, code: "TOTAL", error: "total > 0");
var m = new MessageModel(code: "E003", message: "Timeout");
```

Only calls that name the argument `key:` break. They fail with `CS1739: The best overload for '...' does not have a parameter named 'key'`. It is a compile error and cannot be suppressed, so rename the argument.

Not affected:

- Positional calls, for example `result.AddError("E001", "Order not found")`.
- Callers already compiled against 3.0.x. Parameter names are not part of the binary call signature.

Custom implementations of the interfaces do break, see [section 4](#4-custom-implementations-of-imessagemodel--iresulterrormodel).

## 3. Wire format: `key` / `<Key>` to `code` / `<Code>`

Every serializer now writes `Code` and leaves `Key` out. The exception is .NET Framework builds, see [below](#net-framework-net45-to-net48).

| Serializer                                       | Type                                    | Before       | After         |
| ------------------------------------------------ | --------------------------------------- | ------------ | ------------- |
| System.Text.Json                                 | `MessageModel` / `IMessageModel`        | `"key"`      | `"code"`      |
| System.Text.Json                                 | `ResultErrorModel` (no name attribute)  | `"Key"`      | `"Code"` (default options) |
| Newtonsoft.Json                                  | `MessageModel`, `ResultErrorModel`      | `"Key"`      | `"Code"`      |
| XmlSerializer                                    | `MessageModel`                          | `<Key>`      | `<Code>`      |
| DataContractSerializer / SOAP (`ToSoapResult`)   | `MessageModel`                          | `<Key>`      | `<Code>`      |

```text
Before (System.Text.Json):  { "key": "E001", ... }
After:                      { "code": "E001", ... }
```

```xml
<!-- Before (XmlSerializer) -->
<MessageModel><Key>E001</Key>...</MessageModel>

<!-- After -->
<MessageModel><Code>E001</Code>...</MessageModel>
```

> Old payloads fail silently: a payload that still carries `"key"` or `<Key>` deserializes without error, but `Code` is `null`.

If you have data or consumers in the old format:

- Stored payloads (databases, caches, queues, files): re-serialize them with the new version, or rename the field before the new version reads them (`key` to `code`, `<Key>` to `<Code>`).
- Producers or consumers you do not control: map `key` to `code` at the boundary. An API gateway, a custom converter, or a DTO that you translate into `MessageModel` all work.
- Tests and contracts that assert on the raw payload (JSON schemas, OpenAPI specs, WSDL consumers, snapshot tests): update the property or element name.
- Serializers generated ahead of time against 3.0.x: regenerate them. This covers System.Text.Json source generation (`JsonSerializerContext`) and sgen `*.XmlSerializers.dll`.

### .NET Framework (net45 to net48)

The library references System.Text.Json only for its netstandard targets, so the .NET Framework builds carry no System.Text.Json attributes. That includes `[JsonPropertyName("code")]` and `[JsonIgnore]`. If a .NET Framework app serializes the models with the System.Text.Json NuGet package or with `JavaScriptSerializer` (MVC 5 `Json()`, for example), both `Code` and `Key` are written, in PascalCase. `MessageModel` also binds `Key` on input. `ResultErrorModel.Key` is get-only, so it is never bound.

To avoid this, serialize with Newtonsoft.Json, DataContractSerializer or XmlSerializer, which honor the ignore attributes. Otherwise drop or ignore `Key` at the boundary.

## 4. Custom implementations of `IMessageModel` / `IResultErrorModel`

Both interfaces gain a `Code` member. If you implement them yourself, add it, otherwise the compiler reports `CS0535`.

A type compiled against 3.0.x that implements either interface throws `TypeLoadException` (missing `get_Code`) when it is loaded against the new version. There is no binary-compatible way around this, so recompile it.

```csharp
// Before
public sealed class MyMessage : IMessageModel
{
    public string Key { get; set; }
    // ...
}

// After
public sealed class MyMessage : IMessageModel
{
#if !NETFRAMEWORK
    [JsonPropertyName("code")]
#endif
    [XmlElement("Code")]
    public string Code { get; set; }

    // Keep while the interface still declares the alias. Every attribute is required.
    [Obsolete("Use Code instead. Key will be removed in a future major version.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [XmlIgnore]
    [IgnoreDataMember]
#if !NETFRAMEWORK
    [JsonIgnore]
#endif
    public string Key { get => Code; set => Code = value; }
    // ...
}
```

Copy the whole attribute stack. Interface attributes are not inherited by implementing classes, so a bare `Key` on your class, serialized as that class, is a second serializable and bindable name for the same value. (Through `Result<T>.Messages`, whose declared type is the interface, the interface's `[JsonIgnore]` does apply.) `[IgnoreDataMember]` is the only one of these attributes that Newtonsoft.Json honors.

The sample needs `System`, `System.ComponentModel`, `System.Runtime.Serialization`, `System.Xml.Serialization` and, outside .NET Framework, `System.Text.Json.Serialization`. If your .NET Framework project references System.Text.Json, drop the `#if` so `[JsonIgnore]` applies there too. Only SDK-style projects define `NETFRAMEWORK`. In a legacy (non-SDK) .NET Framework csproj the symbol is not defined, so remove the `#if` blocks there or reference System.Text.Json.

`IResultErrorModel` needs a read-only `string Code { get; }`. While the alias is on the interface, also keep `string Key { get; }` with the same ignore attributes.

## 5. Mixed-version rollout

While services on 3.0.x and on the new version exchange payloads, the code is lost in both directions:

- A new reader gets `"key"` / `<Key>` from an old writer, so `Code == null`.
- An old reader gets `"code"` / `<Code>` from a new writer, so `Key == null`. SOAP clients that validate against the schema (JAX-WS, for example) may fault on the unexpected `<Code>` element instead.

Anything that matches on a code stops working: `result.HasErrorCode("AUTH_DENIED")`, a `switch` on `Code`, a lookup of localized text by code. `HasErrorCode` compares with `Code.Equals(...)`, so it throws `NullReferenceException` on the first message with a null code. It returns `false` only when no message matches and none has a null code. That null handling is unchanged in this release.

Null-safe comparisons fail open instead. `IsSuccess` and `MessageType` are not affected and keep working across versions. Gate on them, and treat the code as extra detail:

```csharp
// Fails open while old writers are still around: the code is null, the
// comparison is false, nothing is denied. HasErrorCode would throw here.
if (!result.Messages.Any(m => m.Code == "AUTH_DENIED"))
    Proceed();

// Safer
if (result.IsSuccess)
    Proceed();
```

Upgrade every reader and writer of a payload together. If that is not possible, map `key` to `code` at the boundary until every party runs the new version.

---

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
| `Validate(predicate, error)`    | Always evaluates; chain to **accumulate** every violation. Overloads for `(code, error)` and `MessageDataModel`. |
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
