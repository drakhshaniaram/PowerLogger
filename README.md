# PowerLogger
 Dynamics AX extention which enables developer to log different errors and outputs very easly in three mediums: Database, Files system, EvenetViewer.

## Features
* Memory efficient: by using of Singleton design pattern
* Extensible: by using of X++ Interfaces
* Easy usability: by using of method chaining technique
* Isolated transactioal scope: by using a parallel transcation that doesn not affect the mai transaction going on

## Sample job for using the facility
```csharp
static void TEST_PowerLogger(Args _args)
{
    PowerLogger::instance().LogOnDB().write("Sample error text.", funcName(), 0, "My sample title");
}
```