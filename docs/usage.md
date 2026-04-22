# Generic result message - USING

For a specific method that you want to use for this type of result/response, you will change the type of method as you can see below:

```csharp
public Result<Foo> GetFooById(Guid id);

public IResult<Foo> GetFooById(Guid id);

public IResult<IEnumerable<Foo>> GetAllFoos();

public IResult AddFoo(Foo foo);

public Result AddFoo(Foo foo);

public IResult<Guid> AddFoo(Foo foo);
```

Another example of using a generic type of result:

```csharp
public IResult<Foo> GetFooById(Guid id)
        {
            try
            {
                var data = _ctx.Foos.FirstOrDefault(x => x.Id == id);

                return data != null
                    //return success result with data
                    ? Result<Foo>.Success(data)
                    //return warning with the message that no existing data
                    : Result<Foo>.Warn("No foo found by specified id");
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Internal error on get foo with id: '{id}'");
                
                //return fail method execution 
                return Result<Foo>.Failure("An error occurred. Please try again.");
            }
        }
```

```csharp
public IResult<Foo> GetFoo(object id)
        {
            var foo = _ctx.Set<Foo>.FirstOrDefault(x=>x.Id == id);
            if (foo != null)
                //return success message with data
                return Result<Foo>.Success(foo);
            else
                //return not found message
                return Result<Foo>.NotFound("Record not found");
        }
```

```csharp
public async Task<IResult> AddFooAsync(Foo request, CancellationToken cancellationToken
         = default)
        {
            try
            {
                await _ctx.Foos.AddAsync(request, cancellationToken);
                await _ctx.SaveChangesAsync(cancellationToken);
                
                //return success message with data
                return Result.Success();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Internal error on add foo");

                //return fail method execution 
                return Result.Failure("Can't add foo");
            }
        }
```

```csharp
public Result<Foo> GetFooById(Guid id)
        {
            try
            {
                var data = _ctx.Foos.FirstOrDefault(x => x.Id == id);

                return data != null
                    //return success result with data
                    ? Result<Foo>.Success(data)
                    //return warning with the message that no existing data
                    : Result<Foo>.Warn("No foo found by specified id");
            }
            catch(Exception e)
            {
                //Return result data as error with exception in messageses
                return e;
            }
        }
```

```csharp
public async Task<Result> AddFooAsync(Foo request, CancellationToken cancellationToken
         = default)
        {
            try
            {
                await _ctx.Foos.AddAsync(request, cancellationToken);
                await _ctx.SaveChangesAsync(cancellationToken);
                
                //return success message with data
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Internal error on add foo");

                return e;
            }
        }
```

For multiple messages on the response from the box are available methods: `AddMessage`, `AddInfo`, `AddInfoConfirm`, `AddNotFound`, `AddWarning`, `AddWarningConfirm`, `AddException`, `AddError`, `AddErrorConfirm`.
An example of using:
```csharp
return Result.Failure()
                .AddInfo("Message 1")
                .AddError("Message 2");
```

For SOAP result services
```csharp
public SoapResult SoapSuccess()
        {
            return Result
            .Success()
            //Cast result 'Result' or 'Result<T>' to XML result
            .ToSoapResult();
        }
```

Below are examples of using fluent configuration access and returning results to the user.
```csharp
return Result.Failure()
                .WithMessage("Message 1")
                .WithKeyCode("Code 1")
                .WithCodeMessage("Code", "Message")
                .WithError("Error message x01")
                .WithError("Error message", "Error code")
                .WithError(new ResultError("code", "message"))
                .WithError(new Exception("Exception"))
                .WithErrors(new List<ResultError>()
                {
                    new ResultError("Error code x1", "Error message x1"),
                    new ResultError("Error code x2", "Error message x2"),
                    new ResultError(new Exception("Exception message"))
                });
```

For efficiency and flexibility, you can find the following methods: `ActionOnSuccess`, `ActionOnFailure`, `ActionOn`, and `ExecuteAction` which allow the execution of custom actions.
-> `ActionOnSuccess` - Execute action only when the Result has success execution;
-> `ActionOnFailure` - Execute action only when the Result has failed execution;
-> `ActionOn` - Specify action/s execution for success and failure request execution;
`ExecuteAction` ->  Execute specified action/s in any case.
```csharp
public IResult<Foo> GetFoo(int recordId)
    {
        var result = _service.GetRecord(recordId);
        result.ActionOnFailure(x =>
            {
                _logger.LogFailureExecution(result);
            });
        
        return result;
    }
    
public IResult<Foo> GetFooX(int recordId)
    {
        return _service
        .GetRecord(recordId)
        .ActionOnFailure(x =>
            {
                _logger.LogFailureExecution(result);
            });
    }
```

```csharp
public IResult<Foo> GetFoo(int recordId)
    {
        var result = _service.GetRecord(recordId);
        result.ActionOnSuccess(x =>
            {
                _logger.LogExecution($"By user {_identity.UserName} request at the record with id: {recordId} obtained info: {result.Response.ToJson}");
            });
        
        return result;
    }
```
The same situation and implementation can be used for `ActionOn`, `ExecuteAction` with specifying necessary action/s.

An example how to user `RelatedObject` in result:
```csharp
public async Task<Result> AddFooAsync(Foo request, CancellationToken cancellationToken
         = default)
        {
            try
            {
                await _ctx.Foos.AddAsync(request, cancellationToken);
                await _ctx.SaveChangesAsync(cancellationToken);
                
                //return success message with data
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Internal error on add foo");

                return Result.
                .Failure("Internal error on add new foo", relatedObjects: new RelatedObjectModel(nameof(AddFooAsync), "Foos"))
                .AddError(e);
            }
        }
```

---

## Modern combinators (recommended)

The legacy `ActionOnSuccess` / `ActionOnFailure` / `ActionOn` helpers and the `Func<Task<TResult>>`-taking `Function*` overloads are now `[Obsolete]`. The combinators below are the recommended replacements, they return values, fail fast on null arguments, and (for async) consistently use `ConfigureAwait(false)`.

### Map / Bind / Match / Tap (synchronous)

These live as instance methods on `Result<T>`.

```csharp
// Map: project Response on success; failure (and its messages) propagate unchanged.
Result<int> length = Result<string>.Success("hello").Map(s => s.Length); // length.Response == 5

// Bind: flat-map. The binder may return a successful or failed Result<TOut>.
//       Returning null from the binder throws InvalidOperationException — return Failure(...) instead.
Result<Order> order = Result<int>.Success(orderId)
    .Bind(id => _orderService.GetOrder(id));   // returns Result<Order>

// Match: fold both branches into a single value (e.g. an HTTP status, a DTO, etc.).
int httpStatus = result.Match(
    onSuccess: _   => 200,
    onFailure: r   => r.HasAnyErrors() ? 400 : 500);

// Tap: side-effect only on success; returns the same instance for chaining.
result.Tap(value => _logger.LogInformation("Loaded {Value}", value));
```

Composition:

```csharp
var pipeline = Result<int>.Success(2)
    .Map(x => x * 5)                          // Result<int>(10)
    .Tap(x => _logger.LogDebug("step={X}", x))
    .Bind(x => Result<string>.Success($"n={x}")); // Result<string>("n=10")
```

### MapAsync / BindAsync / TapAsync (async)

Available as extension methods in `RzR.ResultMessage.Extensions.Result`. Every async overload exists in two source forms; on `Result<T>` and on `Task<Result<T>>` and accepts both sync and async delegates, so an entire async pipeline can be expressed as a single fluent chain:

```csharp
using RzR.ResultMessage.Extensions.Result;

var dto = await _repo.GetOrderByIdAsync(id)             // Task<Result<Order>>
    .MapAsync(o => o.ToSummary())                       // sync projection
    .BindAsync(async s => await _enricher.EnrichAsync(s))  // async bind
    .TapAsync(async s => await _audit.WriteAsync(s))    // async side-effect on success
    .MatchAsync(
        onSuccess: s => OrderResponse.From(s),
        onFailure: r => OrderResponse.Error(r.GetFirstError()));
```

For sync-only callers, the new `FunctionExtensionsAsync` static class also exposes typed async equivalents of the legacy `FunctionOn*` helpers: `FunctionOnSuccessAsync`, `FunctionOnFailureAsync`, `FunctionOnAsync`, `ExecuteFunctionAsync`.

### Validation aggregation: `Validate` / `Ensure`

Two complementary modes for input/business-rule validation, both fluent on `Result<T>`:

| Method | Behavior |
|---|---|
| `Validate(predicate, error)` | **Always** evaluates the predicate. On `false` it appends an error and flips `IsSuccess` to `false`, chain calls to **accumulate every violation** in one pass. Overloads: `(predicate, key, error)`, `(predicate, MessageDataModel)`. |
| `Ensure(predicate, error)`   | **Short-circuits**: skips the predicate once `IsSuccess` is already `false`. |

Accumulating example:

```csharp
var result = Result<Order>.Success(order)
    .Validate(o => o.Items.Count > 0, "Order must contain at least one item")
    .Validate(o => o.Total > 0, "Order total must be positive")
    .Validate(o => o.Customer != null, "Order must have a customer");

// If all three rules fail, result.Messages contains all three errors and IsSuccess == false.
```

Short-circuit guard chain:

```csharp
var result = Result<User>.Success(user)
    .Ensure(u => u != null, "user is required")
    .Ensure(u => !string.IsNullOrEmpty(u.Email), "email is required") // safe: skipped if u == null
    .Ensure(u => u.Email.Contains("@"), "email must be valid");
```

Async variants (`ValidateAsync`) are available on both `Result<T>` and `Task<Result<T>>` and accept either a sync or async predicate.

### `Result<T>.Create()` factory

`Result<T>.Instance` is now `[Obsolete]` because each access returned a *new* instance — a misleading name. Use the explicit factory:

```csharp
var r = Result<Foo>.Create();   // identical behavior, clearer intent
```

### Other behavior changes worth noting

- **Implicit `Exception` -> `Result` / `Result<T>` operators** now throw `ArgumentNullException` when given a `null` exception (previously silently produced an empty failure).
- **`Result<T>(Exception)` constructor** now adds a single `MessageType.Exception` message (previously two messages: an empty info plus the trace).
- **`GetFirstMessage` / `GetFirstError`** fall back to the first `Exception` message when no plain message is present, so exception-only failures no longer surface as empty strings.
- **`JoinErrors`** correctly sets `IsSuccess` from the joined collection (previously inherited it from the calling instance).
- **`Success<T>(T, params RelatedObjectModel[])`** no longer adds a ghost `MessageType.None` message when no related objects are supplied.
- **`ExceptionHelper.PreserveStackTrace`** has been rewritten on top of `ExceptionDispatchInfo.Capture` — AOT-friendly and free of the `SYSLIB0050` warning.
- **`RelatedObjectModel.ToString()`** is now null-safe when `InDataSourceNames` is `null`.
