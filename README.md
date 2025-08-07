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

### License

This project is licensed under the MIT License.

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

## Equality Comparers

Test Fabric provides a comprehensive set of equality comparers designed to simplify testing scenarios where precise
equality comparisons are needed. These are particularly useful with assertion methods like `Assert.Equal` that accept
custom equality comparers.

### Object Equality Comparer

Base class for creating custom object comparers with specific equality rules.

```csharp
// Custom object comparer implementation
public class UserIdComparer : ObjectEqualityComparer<User>
{
    protected override bool EqualsImpl(User x, User y)
    {
        return x.Id == y.Id;
    }
}

// Usage in assertions
var comparer = new UserIdComparer();
var user1 = new User { Id = 1, Name = "John", Email = "john@test.com" };
var user2 = new User { Id = 1, Name = "Jane", Email = "jane@test.com" };

Assert.Equal(user1, user2, comparer); // true - same ID despite different names/emails

// Use cases:
[Fact]
public void Should_Find_User_By_Id_Regardless_Of_Other_Properties()
{
    // Arrange
    var repository = new UserRepository();    
    var expectedUser = new User { Id = 42, Name = "John Doe", Email = "john.doe@example.com" };
    
    // Act
    var foundUser = repository.FindById(42);
    
    // Assert - Compare only by ID, ignoring other properties
    Assert.Equal(expectedUser, foundUser, new UserIdComparer());
}
```

### Compare Factory

The `Compare` class provides a factory for creating commonly used equality comparers:

#### Double Comparers

##### Absolute Double Comparer

Compares double values within an absolute tolerance. Perfect for testing floating-point calculations where you need to
account for precision errors.

```csharp
// Factory method
IEqualityComparer<double> comparer = Compare.DoubleAbsolute(0.01);

// Usage in assertions
Assert.Equal(1.001, 0.999, Compare.DoubleAbsolute(0.01)); // true - difference is 0.002, within tolerance
Assert.Equal(3.14159, 3.14, Compare.DoubleAbsolute(0.002)); // true - difference is 0.00159, within tolerance
Assert.Equal(10.0, 9.5, Compare.DoubleAbsolute(0.1)); // false - difference is 0.5, exceeds tolerance

// Use cases:
[Fact]
public void Should_Calculate_Area_With_Precision()
{
    // Arrange
    var calculator = new CircleAreaCalculator();
    
    // Act
    double result = calculator.CalculateArea(radius: 5.0);
    
    // Assert - Account for floating-point precision errors
    Assert.Equal(78.54, result, Compare.DoubleAbsolute(0.01));
}
```

**When to use:**

- Testing mathematical calculations with known precision requirements
- Comparing results from different calculation methods that should be equivalent
- Unit testing scientific or engineering calculations

##### Relative Double Comparer

Compares double values based on relative percentage difference. Ideal when the tolerance should scale with the magnitude
of the values being compared.

```csharp
// Factory method
IEqualityComparer<double> comparer = Compare.DoubleRelative(0.05); // 5% tolerance

// Usage in assertions
Assert.Equal(100.0, 102.0, Compare.DoubleRelative(0.05)); // true - 2% difference, within 5% tolerance
Assert.Equal(1000.0, 1030.0, Compare.DoubleRelative(0.05)); // true - 3% difference, within 5% tolerance
Assert.Equal(10.0, 12.0, Compare.DoubleRelative(0.05)); // false - 20% difference, exceeds 5% tolerance

// Use cases:
[Fact]
public void Should_Calculate_Growth_Rate_Within_Expected_Range()
{
    // Arrange
    var analyzer = new PerformanceAnalyzer();
    var baselineMetric = 1000.0;
    
    // Act
    double actualGrowth = analyzer.CalculateGrowthRate(baselineMetric);
    double expectedGrowth = 1050.0; // Expected 5% growth
    
    // Assert - Allow 2% variance in growth calculation
    Assert.Equal(expectedGrowth, actualGrowth, Compare.DoubleRelative(0.02));
}
```

**When to use:**

- Testing percentage-based calculations (growth rates, discounts, interest)
- Comparing scaled values where absolute differences would be misleading
- Performance metrics that should be proportionally similar

#### Collection Comparers

##### Array Equality Comparer

Compares arrays element-by-element, supporting custom element comparers.

```csharp
// Basic array comparison
var comparer = new ArrayEqualityComparer<int>();
Assert.Equal(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, comparer); // true

// Array comparison with custom element comparer
var doubleArrayComparer = new ArrayEqualityComparer<double>(Compare.DoubleAbsolute(0.01));
Assert.Equal(
    new[] { 1.001, 2.002, 3.003 }, 
    new[] { 1.000, 2.000, 3.000 }, 
    doubleArrayComparer); // true

// Use cases:
[Fact]
public void Should_Process_Data_Arrays_Correctly()
{
    // Arrange
    var processor = new DataProcessor();
    var input = new[] { 1.1, 2.2, 3.3 };
    var expected = new[] { 2.2, 4.4, 6.6 }; // Input doubled
    
    // Act
    double[] result = processor.DoubleValues(input);
    
    // Assert - Account for floating-point precision in array comparison
    Assert.Equal(expected, result, new ArrayEqualityComparer<double>(Compare.DoubleAbsolute(0.001)));
}
```

##### List Equality Comparer

Similar to array comparer but for `IList<T>` collections.

```csharp
// Basic list comparison
var comparer = new ListEqualityComparer<string>();
Assert.Equal(
    new List<string> { "a", "b", "c" }, 
    new List<string> { "a", "b", "c" }, 
    comparer); // true

// List comparison with custom element comparer
var doubleListComparer = new ListEqualityComparer<double>(Compare.DoubleRelative(0.01));
Assert.Equal(
    new List<double> { 100.0, 200.0 },
    new List<double> { 101.0, 198.0 },
    doubleListComparer); // true (within 1% relative tolerance)

// Use cases:
[Fact]
public void Should_Filter_And_Transform_Lists_Correctly()
{
    // Arrange
    var service = new DataTransformationService();
    var input = new List<decimal> { 10.5m, 20.7m, 30.1m };
    var expected = new List<decimal> { 21.0m, 41.4m, 60.2m };
    
    // Act
    List<decimal> result = service.DoubleAndRound(input);
    
    // Assert
    Assert.Equal(expected, result, new ListEqualityComparer<decimal>());
}
```

### Advanced Scenarios

#### Combining Multiple Comparers

```csharp
[Fact]
public void Should_Compare_Complex_Calculation_Results()
{
    // Arrange
    var calculator = new ScientificCalculator();
    var inputValues = new[] { 1.0, 2.0, 3.0 };
    
    // Expected results with some floating-point tolerance
    var expectedResults = new[] { 2.718, 7.389, 20.086 }; // e^x calculations
    
    // Act
    double[] results = calculator.CalculateExponentials(inputValues);
    
    // Assert - Use array comparer with absolute tolerance for exponential calculations
    var comparer = new ArrayEqualityComparer<double>(Compare.DoubleAbsolute(0.001));
    Assert.Equal(expectedResults, results, comparer);
}

[Fact]
public void Should_Handle_Mixed_Data_Types_In_Collections()
{
    // Arrange
    var processor = new MixedDataProcessor();
    var expectedAmounts = new List<decimal>() { 1.3m, 3.4m, 2.7m };
    var expectedRates = new[] { 0.1, 0.07, 0.12 };
    var expectedAccounts = new List<Account>() { new Account(123), new Account(2453) };
    
    // Act
    var results = processor.ProcessFinancialData();
    
    // Assert different aspects with appropriate comparers
    Assert.Equal(
        expectedAmounts,
        results.Amounts, 
        new ListEqualityComparer<decimal>()); // Exact decimal comparison
    
    Assert.Equal(
        expectedRates,
        results.InterestRates, 
        new ArrayEqualityComparer<double>(Compare.DoubleRelative(0.001))); // Relative comparison for rates
    
    Assert.Equal(
        expectedAccounts, 
        results.Accounts, 
        new ListEqualityComparer<Account>(new AccountNumberComparer())); // Custom object comparison
}
```

### Best Practices

1. **Choose the Right Tolerance**:
    - Use absolute comparers for small numbers or when you know the expected precision
    - Use relative comparers for large numbers or percentage-based calculations

2. **Combine with Collection Comparers**:
    - Wrap element comparers in array/list comparers for collection assertions

3. **Create Custom Object Comparers**:
    - Extend `ObjectEqualityComparer<T>` for domain-specific equality rules

4. **Document Your Tolerances**:
    - Always comment why you chose specific tolerance values in your tests
