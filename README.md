# AsyncAwait

The Task Parallel Library(TPL) and the async/await tandem is the new way to work in .NET. TPL is really a rich library, 
obseleting the Thread class for a developer. The async/await shows us the new way to program asynchronous methods.

This is just a simple WPF example that shows how we can use the aboce concepts to make our app really responsive, without hassle
of any manual threads or background workers. Notice how IProgress is used to notify the progress back to the UI thread.

The standard CancellationToken is used to cancel the async Task. 
The PauseTokenSource by Stephen Toub is also used here, to very elegantly pause the async Task.

