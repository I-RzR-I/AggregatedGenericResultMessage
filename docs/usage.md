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