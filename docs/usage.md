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
public async Task<IResult> AddFooAsync(Foo request, CancellationToken cancellationToken
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