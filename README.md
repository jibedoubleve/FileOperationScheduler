# FileOperationScheduler

> Schedule actions to file or directories and execute them later

# Documentation

## How to use it?

```csharp
// Retrieve an operation scheduler from a default location
// If the file doesn't exist, it'll create one and put default
// values (i.e. no value)
var opScheduler = await OperationSchedulerFactory.RetrieveFromFileAsync(fileName);

// Plan the actions to execute.
// Calling `Commit()` will save the operation
// and they'll be executed next time we call
// ProcessOperations()

var opDir = OperationFactory.RemoveDir(directory, recursive: true);
var opZip = OperationFactory.Unzip(zip, destination);

opScheduler.AddOperation(opDir)
           .AddOperation(opZip)
           .SavePlanAsync();

// Later, after either the machine or the application 
// have been restarted, call ExecutePlanAsync()
// to execute the operations

var scheduler = await OperationSchedulerFactory.RetrieveFromFileAsync(fileName);
await scheduler.ExecutePlanAsync();
```