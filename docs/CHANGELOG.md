### **v.1.0.1.1702** 
-> Was fixed runtime error on using lib in project

### **v.1.0.1.1731** 
-> Changed the readme file, by adding install from NuGet.

### **v.1.0.2.0946** 
-> Clean project and organized for more readable code.<br />
-> Removed local string extension class.<br />
-> Added missing comments.

### **v.1.0.3.1836** 
-> Was added a new message result `Exception`.<br />
-> Was added a new methods for 'Exception': `AddException`, `HasAnyExceptions`.<br />
-> Was added a new method for 'Error result': `HasAnyErrorsOrExceptions`.<br />
-> Was adjusted the method for 'Error result': `GetFirstError` return non null value.<br />
-> Was added a new `operators` for: catch `Exception`, bool or T response on return from methods.<br />
-> Was adjusted the methods from `ResultOfT` for `JoinResults` and added new method `JoinErrorResults`.<br />
-> Was update libs, cleaned code, optimized and reorganized.<br />

### **v.1.0.3.1903** 
-> Libs upgrade and retested some cases.<br />

### **v.1.0.4.1314** 
-> Added support for SOAP services result. Cast from `Result` or `Result<T>` to acceptable result for SOAP (to XML result) in particular use in .net framework.<br />
-> Was added an extension method for SOAP result `ToSoapResult`.<br />
-> Was added support for .net framework 4.6.1 - 4.8.<br />

### **v.1.0.5.1813** 
-> Was added support for .net framework 4.5.<br />

### **v.1.0.6.1706** 
-> Was added new custom responses.<br />
-> Was updated libs.<br />

### **v.1.1.0.0** 
-> Removed from solution reference `DomainCommonExtensions` to reduce package size and allow more dynamic package use without adding unnecessary components.<br/>
-> Added reference to `CodeSource` to set the code reference of the methods used from `DomainCommonExtensions`.<br/>
-> In the `MessageModel` was added a new property named `LogTraceId` as a unique id for the message.<br/>
-> Added more user-friendly result configuration with methods: `WithMessage`, `WithKeyCode`, `WithCodeMessage`, `WithError`, `WithErrors`.<br/>
-> Added new model `ResultError` used in `WithError` and `WithErrors`.<br/>
-> Cleaned up code and reorganization on project structure by extracting functionalities to separate classes.<br/>

### **1.1.1.2048** 
-> Added extension methods(`ActionOnSuccess`, `ActionOnFailure`, `ActionOn`, `ExecuteAction`) which allow executing custom actions on specific cases or in any case you want or need. 