### **v2.0.0.4127** [[RzR](mailto:108324929+I-RzR-I@users.noreply.github.com)] 22-04-2026
> **Major release — contains breaking changes.** See [migration-guide.md](migration-guide.md) for full before/after porting steps.

**Breaking changes**
* [DEV] - (RzR) -> Change namespace from `AggregatedGenericResultMessage` to `RzR.ResultMessage`.
* [FIX] - (RzR) -> Implicit `Exception -> Result` / `Result<T>` operators now throw `ArgumentNullException` on `null` (was: silent empty failure).
* [FIX] - (RzR) -> `Result<T>(Exception)` constructor now adds **one** `MessageType.Exception` message (was: two: empty info + trace).
* [FIX] - (RzR) -> `JoinErrors(IEnumerable<Result>)` now derives `IsSuccess` from the joined collection (was: inherited from caller).
* [FIX] - (RzR) -> `Success<T>(T, params RelatedObjectModel[])` no longer adds a ghost `MessageType.None` message when `relatedObjects` is empty/null.
* [DEV] - (RzR) -> `ExceptionHelper.PreserveStackTrace` signature changed: `void` -> `ExceptionDispatchInfo`
* [DEV] - (RzR) -> `GetFirstMessage` / `GetFirstError` now fall back to the first exception message when no plain message exists (was: empty string).

**Obsoletions** (compile warnings; will fail with `TreatWarningsAsErrors`)
* [DEV] - (RzR) -> `Result<T>.Instance` -> use `Result<T>.Create()`.
* [DEV] - (RzR) -> `ActionOnSuccess` / `ActionOnFailure` / `ActionOn` (4 overloads) -> use `Tap` / `Match`.
* [DEV] - (RzR) -> All 7 `FunctionOn*` / `ExecuteFunction` overloads taking `Func<Task<TResult>>` -> use `FunctionExtensionsAsync.*Async` (avoids sync-over-async deadlock).

---

* [DEV] - (RzR) -> `Map<TOut>(Func<T,TOut>)`, `Bind<TOut>(Func<T,Result<TOut>>)`, `Match<TOut>(Func<T,TOut>, Func<Result<T>,TOut>)`, `Tap(Action<T>)`.
* [DEV] - (RzR) -> `MapAsync`, `BindAsync`, `TapAsync`, `MatchAsync`, overloads on both `Result<T>` and `Task<Result<T>>`, accepting sync and async delegates. All `ConfigureAwait(false)`.
* [DEV] - (RzR) -> `Tap(this Task<Result<T>>, Action<T>)` for sync side-effect on awaited results.
* [DEV] - (RzR) -> New static class `FunctionExtensionsAsync` exposing `FunctionOnSuccessAsync`, `FunctionOnFailureAsync`, `FunctionOnAsync` (4 overloads), `ExecuteFunctionAsync`.
* [DEV] - (RzR) -> `Validate(Func<T,bool>, error)`- accumulates every violation in one chain pass; overloads for `(predicate, key, error)` and `(predicate, MessageDataModel)`.
* [DEV] - (RzR) -> `Ensure(Func<T,bool>, error)` — short-circuits once `IsSuccess == false`.
* [DEV] - (RzR) -> `ValidateAsync` — async predicate on both `Result<T>` and `Task<Result<T>>`.
* [DEV] - (RzR) -> Match<TOut>(Func<IResult,TOut>, Func<IResult,TOut>)` and parameterless / `Action`-based overloads.
* [DEV] - (RzR) -> `MatchAsync<TOut>(Func<IResult,Task<TOut>>, Func<IResult,Task<TOut>>)`.
* [FIX] - (RzR) -> `RelatedObjectModel.ToString()` is now null-safe when `InDataSourceNames` is `null`

### **v1.4.1.8497** [[RzR](mailto:108324929+I-RzR-I@users.noreply.github.com)] 24-02-2026
* [f47b134] (RzR) -> Auto commit uncommited files
* [6a0d48a] (RzR) -> Fix the `IsFailure` flag value.

### **v1.4.0.7788** [[RzR](mailto:108324929+I-RzR-I@users.noreply.github.com)] 09-02-2026
* [f68e0f7] (RzR) -> Auto commit uncommited files
* [1d311f9] (RzR) -> Add new script for version gen.
* [c438299] (RzR) -> Add `IsFailure` in result. Add new extension methods: `WithMessages`, `ReturnAutoSuccessOrFailure`

### **v1.3.5.4696**
-> Upgrade version for the 'CodeSource' package.<br />

### **v1.3.4.6865**
-> Upgrade minimum `System.Text.Json` package required version to `6.0.10`, fixing CVE (`CVE-2024-43485`).<br />

### **v1.3.3.6068** 
-> Adjust the location for methods: `GetFirstMessage`, `GetFirstMessageWithDetails`.<br />
-> Add a new extension for result execution `FunctionExtensions` (`FunctionOnSuccess`, `FunctionOnFailure`, `FunctionOn`, `ExecuteFunction`), which allows to execute of one or more functions.<br />

### **v1.3.2.469** 
-> Add `RelatedObject` in code (means related object in code execution, usually used in case of some errors to show method name, stored procedure, table, etc).<br />
-> Adjust code to solution code style.<br />
-> Adjust exposed methods and add a few new.

### **v1.2.2.2040** 
-> Adjust code to solution code style.<br />
-> Update lib versions.

### **v.1.2.1.0** 
-> Hide internal used extension.

### **v.1.2.0.0** 
-> Adjust JSON result/response property name (`result` => `response`).

### **1.1.1.2048** 
-> Added extension methods(`ActionOnSuccess`, `ActionOnFailure`, `ActionOn`, `ExecuteAction`) which allow executing custom actions on specific cases or in any case you want or need. 

### **v.1.1.0.0** 
-> Removed from solution reference `DomainCommonExtensions` to reduce package size and allow more dynamic package use without adding unnecessary components.<br/>
-> Added reference to `CodeSource` to set the code reference of the methods used from `DomainCommonExtensions`.<br/>
-> In the `MessageModel` was added a new property named `LogTraceId` as a unique id for the message.<br/>
-> Added more user-friendly result configuration with methods: `WithMessage`, `WithKeyCode`, `WithCodeMessage`, `WithError`, `WithErrors`.<br/>
-> Added new model `ResultError` used in `WithError` and `WithErrors`.<br/>
-> Cleaned up code and reorganization on project structure by extracting functionalities to separate classes.<br/>

### **v.1.0.6.1706** 
-> Was added new custom responses.<br />
-> Was updated libs.<br />

### **v.1.0.5.1813** 
-> Was added support for .net framework 4.5.<br />

### **v.1.0.4.1314** 
-> Added support for SOAP services result. Cast from `Result` or `Result<T>` to acceptable result for SOAP (to XML result) in particular use in .net framework.<br />
-> Was added an extension method for SOAP result `ToSoapResult`.<br />
-> Was added support for .net framework 4.6.1 - 4.8.<br />

### **v.1.0.3.1903** 
-> Libs upgrade and retested some cases.<br />

### **v.1.0.3.1836** 
-> Was added a new message result `Exception`.<br />
-> Was added a new methods for 'Exception': `AddException`, `HasAnyExceptions`.<br />
-> Was added a new method for 'Error result': `HasAnyErrorsOrExceptions`.<br />
-> Was adjusted the method for 'Error result': `GetFirstError` return non null value.<br />
-> Was added a new `operators` for: catch `Exception`, bool or T response on return from methods.<br />
-> Was adjusted the methods from `ResultOfT` for `JoinResults` and added new method `JoinErrorResults`.<br />
-> Was update libs, cleaned code, optimized and reorganized.<br />

### **v.1.0.2.0946** 
-> Clean project and organized for more readable code.<br />
-> Removed local string extension class.<br />
-> Added missing comments.

### **v.1.0.1.1731** 
-> Changed the readme file, by adding install from NuGet.

### **v.1.0.1.1702** 
-> Was fixed runtime error on using lib in project
