# Test Fabric

## Introduction

Testing toolkit leveraging Moq and AutoFixture to simplify creating unit and integration tests in .NET projects.

### Benefits

- **Fluent API**: Chain method calls for readable test setup
- **Type Safety**: Compile-time checking of mock configurations
- **Reduced Boilerplate**: Less repetitive mock setup code
- **Built-in Patterns**: Common interfaces already implemented
- **Moq Integration**: Full access to underlying Moq functionality when needed

### Requirements

- .NET Standard 2.0 or higher, .NET Framework 4.6.2 or higher, .NET 5 or higher
- Moq 4.20.72 or higher
- AutoFixture 4.18.1 or higher
- Microsoft.Extensions.Logging.Abstractions 8.0.0 or higher

### Installation

```
dotnet add package TestFabric
```

## MockBuilder&lt;T&gt;

The `MockBuilder<T>` is a base class that provides helper methods
to provide a fluent interface for configuring mock objects. It supports:

- **Properties**: Get-only and read/write property configuration
- **Methods**: Action and function method configuration with various parameter counts
- **Async Methods**: Async action and function method configuration
- **Try-Get Patterns**: Methods that return bool and have out parameters

### Basic Usage

You can create a mock builder for an interface.

```csharp
public interface IUserRepository
{
    User GetById(int id);
    Save(User user);
}
```

By calling helper methods in configuration methods.
Returning the instance provides a fluent interface to the mock builder.

```csharp
using TestFabric;

public class IUserRepositoryMockBuilder : MockBuilder<IUserRepository>
{
    public IUserRepositoryMockBuilder WithGetById(User user) 
    {
        WithFunction(x => x.GetById(It.IsAny<int>()), user); 
        return this;
    }
    
    public IUserRepositoryMockBuilder WithSave()
    {
        WithAction(x => x.Save(It.IsAny<User>()));
        return this;
    }
}
```

You can then easily configure your mock in your tests.

```csharp
[Fact]
public void ShouldSave()
{
    // Arrange
    var user = new User { Id = 1, Name = "John" }; 
    var mockRepository = newUserRepositoryMockBuilder()
        .WithGetById(user)
        .WithSave()
        .Create();
    var service = new UserService(mockRepository.Object);

    // Act
    service.UpdateUser(1, "Jane");

    // Assert
    mockRepository.Verify(x => x.Save(It.IsAny<User>()), Times.Once);
}
```

### MockBuilder Methods

#### Properties

```csharp 
    // Read/Write Property
    WithProperty(x => x.Name, "Initial Value");
    // Get-only Property with fixed value
    WithPropertyGet(x => x.Id, 42);
    // Get-only Property with delegate
    WithPropertyGet(x => x.Timestamp, () => DateTime.Now);  
``` 

#### Actions (Void Methods)

```csharp 
    // Simple action 
    WithAction(x => x.DoSomething());
    // Action with callback 
    WithAction(x => x.Process(It.IsAny<string>()), (value) => Console.WriteLine(value)); 
    // Async action (Task)
    WithActionAsync(x => x.ProcessAsync(It.IsAny<int>()), (id) => Console.WriteLine($"Processing {id}"));
``` 

#### Functions (Methods with Return Values)

```csharp 
    // Function returning fixed value 
    WithFunction(x => x.Calculate(), 100);
    // Function returning sequence of values 
    WithFunction(x => x.GetNext(), 1, 2, 3, 4);
    // Function with delegate 
    WithFunction(x => x.Transform(It.IsAny<string>()), (input) => input.ToUpper()); 
    // Async function (Task<int>)
    WithFunctionAsync(x => x.ComputeAsync(), () => 42);
``` 

#### Try-Get Pattern

```csharp 
    // Method that returns bool and has out parameter 
    WithTryGet(x => x.TryGetValue(out outValue), true, outValue); 
    // With callback for dynamic out value
    WithTryGet<string, int>(
        x => x.TryParse(It.IsAny<string>(), out outValue), 
        true,
        outValue);
``` 

### Advanced Examples

#### Complex Service Testing

```csharp 
public class OrderServiceTests 
{ 
    [Fact] 
    public async Task Should_Process_Order_Successfully() 
    {
        // Arrange 
        var order = new Order { Id = 1, Total = 100.00m }; 
        var logMessages = new List<string>(); 
        var progressValues = new List<string>();  
        var mockRepository = new OrderRepositoryMockBuilder()
            .WithGetById(order)
            .WithSave()
            .Create();
        var mockLogger = new LoggerMockBuilder<OrderService>()
            .WithLog(logMessages)
            .Create();
        var mockProgress = new ProgressMockBuilder<string>()
            .WithReport(msg => progressValues.Add(msg))
            .Create();
    
        var sut = new OrderService(mockRepository.Object, mockLogger.Object);
    
        // Act
        await sut.ProcessOrderAsync(1, mockProgress.Object);
    
        // Assert
        Assert.Contains(logMessages, msg => msg.Contains("Processing order 1"));
        Assert.Contains(progressValues, "Order validated");
        Assert.Contains(progressValues, "Order processed");
        
        mockRepository.Verify(x => x.Save(It.IsAny<Order>()), Times.Once);
    }
}
``` 

#### Testing Error Scenarios

```csharp
[Fact]
public void Should_Handle_Repository_Exception() 
{ 
    // Arrange 
    var mockRepository = new UserRepositoryMockBuilder()
        .WithGetById(_ => throw new DatabaseException("Connection failed"))
        .Create();
    var sut = new UserService(mockRepository.Object);

    // Act & Assert
    Assert.Throws<DatabaseException>(() => service.GetUser(1));
}
``` 

## Provided Mock Builders

Test Fabric includes pre-built mock builders for common .NET interfaces:

### LoggerMockBuilder<T>

Simplifies mocking `ILogger<T>` for testing logging behavior.

```csharp 
[Fact]
public void Should_Log_Error_Messages() 
{ 
    // Arrange 
    var logMessages = new List (); 
    var mockLogger = new LoggerMockBuilder<MyService>()
        .WithLog(logMessages)
        .Create();  
    var service = new MyService(mockLogger.Object);

    // Act
    service.ProcessWithError();

    // Assert
    Assert.Contains(logMessages, msg => msg.Contains("[Error]"));
}
``` 

### ProgressMockBuilder<T>

Mocks `IProgress<T>` for testing progress reporting functionality.

```csharp 
[Fact]
public async Task Should_Report_Progress() 
{ 
    // Arrange 
    var progressValues = new List<int>(); 
    var mockProgress = new ProgressMockBuilder<int>()
        .WithReport(value => progressValues.Add(value))
        .Create();  
    var processor = new DataProcessor();

    // Act
    await processor.ProcessDataAsync(mockProgress.Object);

    // Assert
    Assert.Equal(new[] { 0, 25, 50, 75, 100 }, progressValues);
}
``` 

### EqualityComparerMockBuilder<T>

Mocks `IEqualityComparer<T>` for testing custom equality logic.

```csharp 
[Fact]
public void Should_Use_Custom_Equality() 
{ 
    // Arrange 
    var mockComparer = new EqualityComparerMockBuilder<User>()
        .WithEquals((u1, u2) => u1.Id == u2.Id)
        .WithGetHashCode(u => u.Id.GetHashCode())
        .Create(); 
    var set = new HashSet<User>(mockComparer);
    var user1 = new User { Id = 1, Name = "John" };
    var user2 = new User { Id = 1, Name = "Jane" };

    // Act
    set.Add(user1);
    set.Add(user2);

    // Assert
    Assert.Single(set); // Only one user because they have the same ID
}
``` 

## License

This project is licensed under the MIT License.