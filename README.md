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

### Provided Mock Builders

Test Fabric includes pre-built mock builders for common .NET interfaces:

#### LoggerMockBuilder<T>

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

#### ProgressMockBuilder<T>

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

#### EqualityComparerMockBuilder<T>

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

Compares double values based on relative percentage difference. Ideal when the tolerance should scale with the size
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

### Equality Comparer Builder

The `EqualityComparerBuilder<T>` provides a fluent API for creating custom equality comparers with fine-grained control
over how objects are compared. It's particularly useful for testing scenarios where you need to compare complex objects
with specific equality rules.

The builder allows you to:

- **Configure custom comparers for specific types**
- **Ignore specific members during comparison**
- **Set custom comparison logic for individual properties/fields**
- **Enable detailed tracing for debugging comparison failures**
- **Handle collections (arrays, lists) automatically**

The Equality Comparer Builder provides the flexibility to create precise comparison logic that matches your testing
needs, making assertions more reliable and maintainable.

#### Simple Object Comparison

```csharp
[Fact]
public void Should_Compare_Users_By_Custom_Logic()
{
    // Arrange
    var comparer = new EqualityComparerBuilder<User>()
        .Create();
    
    var user1 = new User { Id = 1, Name = "John", Email = "john@test.com" };
    var user2 = new User { Id = 1, Name = "John", Email = "john@test.com" };
    
    // Act & Assert
    Assert.Equal(user1, user2, comparer);
}
```

#### Ignoring Specific Members

```csharp
[Fact]
public void Should_Ignore_Timestamp_During_Comparison()
{
    // Arrange
    var comparer = new EqualityComparerBuilder<LogEntry>()
        .IgnoreMember(x => x.Timestamp)
        .IgnoreMember(x => x.Id)
        .Create();
    
    var entry1 = new LogEntry 
    { 
        Id = 1, 
        Message = "Error occurred", 
        Level = LogLevel.Error,
        Timestamp = DateTime.Now 
    };
    var entry2 = new LogEntry 
    { 
        Id = 2, 
        Message = "Error occurred", 
        Level = LogLevel.Error,
        Timestamp = DateTime.Now.AddMinutes(5) 
    };
    
    // Act & Assert - Same despite different IDs and timestamps
    Assert.Equal(entry1, entry2, comparer);
}
```

#### Custom Member Comparers

Configure specific comparison logic for individual properties or fields:

```csharp
[Fact]
public void Should_Use_Custom_Member_Comparers()
{
    // Arrange
    var comparer = new EqualityComparerBuilder<Product>()
        .ConfigureMember(x => x.Price, Compare.DoubleAbsolute(0.01))
        .ConfigureMember(x => x.Name, (n1, n2) => string.Equals(n1, n2, StringComparison.OrdinalIgnoreCase))
        .Create();
    
    var product1 = new Product 
    { 
        Name = "Widget", 
        Price = 19.99,
        Category = "Tools" 
    };
    var product2 = new Product 
    { 
        Name = "WIDGET", 
        Price = 19.991,
        Category = "Tools" 
    };
    
    // Act & Assert - Equal despite case difference and minor price variance
    Assert.Equal(product1, product2, comparer);
}
```

#### Type-Level Configuration

Configure comparison logic for entire types:

```csharp
[Fact]
public void Should_Use_Custom_Type_Comparers()
{
    // Arrange
    var comparer = new EqualityComparerBuilder<Order>()
        .ConfigureType<DateTime>((d1, d2) => d1.Date == d2.Date) // Compare dates only, ignore time
        .ConfigureType<decimal>(Compare.DoubleAbsolute(0.01))     // Custom decimal precision
        .Create();
    
    var order1 = new Order 
    { 
        OrderDate = new DateTime(2023, 1, 15, 10, 30, 0),
        Total = 99.99m 
    };
    var order2 = new Order 
    { 
        OrderDate = new DateTime(2023, 1, 15, 16, 45, 0),
        Total = 99.991m 
    };
    
    // Act & Assert - Equal despite different times and minor amount difference
    Assert.Equal(order1, order2, comparer);
}
```

#### Ignoring Types Completely

```csharp
[Fact]
public void Should_Ignore_Audit_Information()
{
    // Arrange
    var comparer = new EqualityComparerBuilder<Document>()
        .IgnoreType<AuditInfo>()  // Ignore all audit info objects
        .Create();
    
    var doc1 = new Document 
    { 
        Title = "Report",
        Content = "Important data",
        Audit = new AuditInfo { CreatedBy = "user1", CreatedAt = DateTime.Now }
    };
    var doc2 = new Document 
    { 
        Title = "Report",
        Content = "Important data", 
        Audit = new AuditInfo { CreatedBy = "user2", CreatedAt = DateTime.Now.AddDays(1) }
    };
    
    // Act & Assert - Equal despite different audit info
    Assert.Equal(doc1, doc2, comparer);
}
```

#### Collection Handling

The builder automatically handles arrays and lists:

```csharp
[Fact]
public void Should_Compare_Collections_With_Custom_Element_Logic()
{
    // Arrange
    var comparer = new EqualityComparerBuilder<OrderSummary>()
        .ConfigureType<decimal>(Compare.DoubleAbsolute(0.01))
        .Create();
    
    var summary1 = new OrderSummary
    {
        ItemPrices = new[] { 10.99m, 25.00m, 5.99m },
        Categories = new List<string> { "Electronics", "Books", "Clothing" }
    };
    var summary2 = new OrderSummary
    {
        ItemPrices = new[] { 10.991m, 24.999m, 5.991m },
        Categories = new List<string> { "Electronics", "Books", "Clothing" }
    };
    
    // Act & Assert - Arrays and lists compared with custom decimal precision
    Assert.Equal(summary1, summary2, comparer);
}
```

#### Debugging with Tracing

Enable tracing to understand why comparisons fail:

```csharp
[Fact]
public void Should_Trace_Comparison_Process()
{
    // Arrange
    var traceOutput = new List<string>();
    var comparer = new EqualityComparerBuilder<ComplexObject>()
        .ConfigureMember(x => x.Value, Compare.DoubleAbsolute(0.001))
        .EnableTracing(line => traceOutput.Add(line), detailed: true)
        .Create();
    
    var obj1 = new ComplexObject { Id = 1, Value = 1.001, Name = "Test" };
    var obj2 = new ComplexObject { Id = 2, Value = 1.002, Name = "Test" };
    
    // Act
    bool areEqual = comparer.Equals(obj1, obj2);
    
    // Assert
    Assert.False(areEqual);
    
    // Examine trace output to understand the failure
    Assert.Contains(traceOutput, line => line.Contains("Check equality of ComplexObject"));
    Assert.Contains(traceOutput, line => line.Contains("member comparison failed"));
}
```

### Test Clock

The `TestClock` class provides a controlled time environment for testing, allowing you to simulate specific points in
time and advance time as needed during tests. This is particularly valuable for testing time-dependent logic,
timestamps, scheduling, and time-based calculations without relying on the system clock.

#### Basic Usage

```csharp
[Fact] 
public void Should_Generate_Timestamps_In_Sequence() 
{ 
    // Arrange 
    var testClock = new TestClock(); 
    var startTime = new DateTimeOffset(2023, 6, 15, 10, 0, 0, TimeSpan.Zero);
    testClock.StartAt(startTime);
    var generator = new TimeStampGenerator(testClock);

    // Act
    var firstTimestamp = generator.GetCurrentTimestamp();
    testClock.Advance(TimeSpan.FromMinutes(30));
    var secondTimestamp = generator.GetCurrentTimestamp();

    // Assert
    Assert.Equal(startTime, firstTimestamp);
    Assert.Equal(startTime.AddMinutes(30), secondTimestamp);
}
``` 

#### Mock Building Integration

TestClock integrates seamlessly with MockBuilder scenarios, enabling precise control over time-dependent mock behaviors:

```csharp 
// Example service that depends on time 
public interface ITimeStampGenerator 
{
    DateTimeOffset GetCurrentTimestamp(); 
    DateTimeOffset GetExpirationTime(TimeSpan validity); 
    bool IsExpired(DateTimeOffset timestamp, TimeSpan maxAge);
}
// Mock builder that uses TestClock for time control 
public class TimeStampGeneratorMockBuilder : MockBuilder<ITimeStampGenerator> 
{ 
    private readonly TestClock _testClock;
    
    public TimeStampGeneratorMockBuilder(TestClock testClock)
    {
        _testClock = testClock;
    }
 
    public TimeStampGeneratorMockBuilder WithCurrentTimestamp()
    {
        WithFunction(x => x.GetCurrentTimestamp(), () => _testClock.UtcNow);
        return this;
    }
 
    public TimeStampGeneratorMockBuilder WithExpirationTime()
    {
        WithFunction<TimeSpan, DateTimeOffset>(
            x => x.GetExpirationTime(It.IsAny<TimeSpan>()), 
            validity => _testClock.UtcNow.Add(validity));
        return this;
    }

    public TimeStampGeneratorMockBuilder WithExpiredCheck()
    {
        WithFunction<DateTimeOffset, TimeSpan, bool>(
            x => x.IsExpired(It.IsAny<DateTimeOffset>(), It.IsAny<TimeSpan>()),
            (timestamp, maxAge) => _testClock.UtcNow - timestamp > maxAge);
        return this;
    }
}
``` 

TestClock can be combined with other mock builders for comprehensive testing scenarios:

```csharp 
[Fact]
public void Should_Log_And_Track_Time_Dependent_Operations() 
{ 
    // Arrange 
    var testClock = new TestClock(); 
    var operationStart = new DateTimeOffset(2023, 7, 20, 11, 0, 0, TimeSpan.Zero); 
    testClock.StartAt(operationStart);
    var logMessages = new List<string>();
    var progressValues = new List<string>();

    var mockLogger = new LoggerMockBuilder<DataProcessor>()
        .WithLog(logMessages)
        .Create();
    
    var mockProgress = new ProgressMockBuilder<string>()
        .WithReport(msg => progressValues.Add(msg))
        .Create();
    
    var mockTimeGenerator = new TimeStampGeneratorMockBuilder(testClock)
        .WithCurrentTimestamp()
        .Create();
    
    var processor = new DataProcessor(
        mockTimeGenerator.Object, 
        mockLogger.Object);

    // Act - Process data with time advancement simulation
    processor.ProcessDataAsync(mockProgress.Object);
    
    testClock.Advance(TimeSpan.FromSeconds(30));
    // Processor internally uses time generator for progress timestamps
    
    testClock.Advance(TimeSpan.FromMinutes(1));
    // Processor logs completion with timestamp
    
    // Assert
    Assert.Contains(logMessages, msg => msg.Contains("Started at"));
    Assert.Contains(logMessages, msg => msg.Contains("Completed at"));
    Assert.Contains(progressValues, "Processing started");
    Assert.Contains(progressValues, "Processing completed");
    var timestampedLogs = logMessages.Where(msg => msg.Contains("11:00")).ToList();
    Assert.NotEmpty(timestampedLogs);
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

## Test Suites

Test Fabric provides test suite base classes that give you access to powerful data generation capabilities through
built-in data factories. These test suites simplify creating test data and provide a consistent approach to randomized
testing.

### TestSuite.Normal

The most important test suite to use is `TestSuite.Normal`, which provides access to a built-in data factory configured
with standard settings. This is the recommended base class for most testing scenarios.

```csharp
public class PersonServiceTests : TestSuite.Normal
{
    [Fact]
    public void Should_Create_Person_With_Valid_Data()
    {
        // Arrange - Use built-in data factory to create test data
        var expectedPerson = Factory.Create<Person>();
        var service = new PersonService();

        // Act
        var result = service.CreatePerson(expectedPerson.Name, expectedPerson.Age);

        // Assert
        Assert.Equal(expectedPerson.Name, result.Name);
        Assert.Equal(expectedPerson.Age, result.Age);
    }

    [Fact] 
    public void Should_Handle_Multiple_People()
    {
        // Arrange - Create multiple test objects
        var people = Factory.CreateMany<Person>(5).ToList();
        var service = new PersonService();

        // Act
        var results = service.ProcessPeople(people);

        // Assert
        Assert.Equal(people.Count, results.Count);
    }
}
```

### Randomized Data Configuration

The data factory in TestSuite classes generates randomized data that helps discover edge cases and makes your tests more
robust. You can configure constraints on the generated data to match your testing needs.

```csharp
public class ProductTests : TestSuite.Normal
{
    [Fact]
    public void Should_Calculate_Discount_For_Various_Prices()
    {
        // Arrange - Generate products with constrained price ranges
        var expensiveProduct = Factory.Build<Product>()
            .With(p => p.Price, Factory.CreateFromRange(100d, 1000d))
            .Create();
            
        var cheapProduct = Factory.Build<Product>()
            .With(p => p.Price, Factory.CreateFromRange(5d, 15d))
            .Create();
            
        var calculator = new DiscountCalculator();

        // Act & Assert
        var expensiveDiscount = calculator.CalculateDiscount(expensiveProduct);
        var cheapDiscount = calculator.CalculateDiscount(cheapProduct);
        
        Assert.True(expensiveDiscount > cheapDiscount);
    }
}
```

### Constrained Building

Use the `BuildConstrained<T>()` method to create objects that should be selected from a range or list of valid values.

```csharp
public class UserValidationTests : TestSuite.Normal
{
    [Fact]
    public void Should_Validate_User_Age()
    {
        // Arrange - Create user with constrained email format
        var validAge = Factory.BuildConstrained<int>()
            .AddOptions(2,7,13)
            .AddRange(new NumberRange<int>(20, 30))
            .Create();            
        var validator = new UserValidator();

        // Act
        var result = validator.ValidateAge(validAge);

        // Assert
        Assert.True(result.IsValid);
    }
}
```

### Test data generation methods

The `TestSuite<T>` class provides a comprehensive set of methods for generating test data,
making it easy to create realistic and varied test scenarios.

#### `Random<T>()`

Generates a completely random object of the specified type with all properties populated with random values.

```csharp 
// Generate random objects
 var user = Random<User>(); 
 var number = Random<int>(); 
 var date = Random<DateTimeOffset>();
``` 

**Use this when:** You need test data but don't care about specific values, just that all properties are populated.

#### `Random<T>(int count)`

Generates multiple completely random objects of the specified type with all properties populated with random values.

```csharp
// Generate multiple random objects
var users = Random<User>(5); // 5 random users 
var numbers = Random<int>(10); // 10 random integers 
var dates = Random<DateTimeOffset>(3); // 3 random dates
``` 

**Use this when:** You need multiple test objects but don't care about specific values, just that all properties are
populated.

#### `InRange<T>(T minInclusive, T maxExclusive)`

Generates a random value within a specific numeric range. The minimum value is included, but the maximum value is
excluded.

```csharp
// Generate values within ranges
var age = InRange(18, 65);           // Age between 18-64
var price = InRange(10.0, 100.0);    // Price between 10.00-99.99
var year = InRange(2020, 2025);      // Year between 2020-2024
var date = InRange(DateTime.Today.AddDays(-7), DateTime.Today); // Date in the last week
```

**Use this when:** You need random data that falls within realistic or valid bounds for your business logic.

#### `InRange<T>(int count, T minInclusive, T maxExclusive)`

Generates multiple random values within a specific numeric range. The minimum value is included, but the maximum value
is excluded.

```csharp
// Generate multiple values within ranges 
var ages = InRange(5, 18, 65); // 5 ages between 18-64 
var prices = InRange(10, 10.0, 100.0); // 10 prices between 10.00-99.99 
var years = InRange(3, 2020, 2025); // 3 years between 2020-2024
``` 

**Use this when:** You need multiple random values that fall within realistic or valid bounds for your business logic.

#### `InRange<T>(IEnumerable<T> items)`

Randomly selects one item from a predefined collection of valid options.

```csharp
// Pick from predefined options
var colors = new[] { "Red", "Green", "Blue", "Yellow" };
var randomColor = InRange(colors);

var validEmails = new[] { "test@example.com", "user@domain.org" };
var email = InRange(validEmails);
```

**Use this when:** You have a specific set of valid values and need to randomly pick one for testing.

#### `InRange<T>(int count, IEnumerable<T> items)`

Randomly selects multiple items from a predefined collection of valid options (with potential duplicates).

```csharp 
// Pick multiple items from predefined options 
var colors = new[] { "Red", "Green", "Blue", "Yellow" }; 
var randomColors = InRange(5, colors); // 5 random colors (may include duplicates)
var validEmails = new[] { "test@example.com", "user@domain.org" }; 
var emails = InRange(3, validEmails); // 3 random emails from the list
``` 

**Use this when:** You need multiple random selections from a specific set of valid values for testing.

#### Personal Information

##### FirstName()

Returns a random first name from a predefined list:

```csharp
var firstName = FirstName(); // "John", "Maria", "Alex", etc.
``` 

##### LastName()

Returns a random last name from a predefined list:

```csharp 
var lastName = LastName(); // "Smith", "Garcia", "Johnson", etc.
``` 

##### FullName(hasSecondLastName)

Generates a complete name combining first and last names:

```csharp
var name = FullName(); // "John Smith" var latinName = FullName(hasSecondLastName: true); // "Maria Garcia Lopez"
``` 

##### Email()

Generates a realistic email address:

```csharp
var email = Email(); // "john.smith@example.com"
``` 

##### CompanyName()

Generates a fictional company name:

```csharp
var company = CompanyName(); // "NimbusWorks", "Aurora Dynamics", etc.
``` 

##### Country()

Returns a random country from a predefined list:

```csharp
var country = Country(); // "USA", "Germany", "Brazil", etc.
``` 

##### City(country)

Returns a random city for the specified country:

```csharp
var country = Country();  // "USA"
var city = City(country); // "New York", "Los Angeles", "Chicago", etc. var germanCity = City("Germany"); // "Berlin", "Munich", "Hamburg", etc.
``` 

#### Date and Time Generation

##### RecentDateTime(daysBack)

Generates a random DateTime within the specified number of days from today:

```csharp
var recentDate = RecentDateTime(); // Within last 30 days (default)
var lastWeek = RecentDateTime(7); // Within last 7 days
``` 

##### RecentDateTime(timeSpan)

Generates a random DateTime within the specified timespan from now:

```csharp
var recentDate = RecentDateTime(TimeSpan.FromDays(7)); // Within last 7 days
var lastHour = RecentDateTime(TimeSpan.FromHours(1));  // Within last hour
var lastMonth = RecentDateTime(TimeSpan.FromDays(30)); // Within last 30 days
``` 

**Use this when:** You need more precise control over the time range for recent dates.

##### RecentDateTimeOffset(daysBack)

Generates a random DateTimeOffset within the specified range:

```csharp
var recentOffset = RecentDateTimeOffset(); // Within last 30 days (default) 
var lastTwoMonths = RecentDateTimeOffset(60); // Within last 60 days
``` 

##### RecentDateTimeOffset(timeSpan)

Generates a random DateTimeOffset within the specified timespan from now:

```csharp
var recentOffset = RecentDateTimeOffset(TimeSpan.FromHours(1));  // Within last hour
var lastWeek = RecentDateTimeOffset(TimeSpan.FromDays(7));       // Within last week
``` 

**Use this when:** You need DateTimeOffset values within a specific timespan with timezone information.

#### Template-Based Generation

##### FromTemplate<T>(template, overrides)

Creates objects based on a template with selective property overrides:

```csharp
var template = new User { Name = "Template", Email = "template@test.com", IsActive = true };
// Create single object with overrides
var user = FromTemplate(template, x => x.Name, x => x.Email); // Result: Random name and email, but IsActive = true from template
// Create multiple objects with overrides 
var users = FromTemplate(5, template, x => x.Name, x => x.Email); // Result: 5 users with random names/emails but same IsActive value
``` 

### Culture Management

Testing applications that handle culture-sensitive data like dates, numbers, and currencies often requires setting
specific cultures to validate formatting and parsing behavior. Test Fabric provides methods to control the culture
context during test execution, ensuring consistent and accurate testing of localization features.

#### SetCurrentCulture(info/culture)

Sets the thread's culture for localized data generation:

```csharp
SetCurrentCulture("es-ES"); // Spanish culture
SetCurrentCulture(CultureInfo.GetCultureInfo("de-DE")); // German culture
```

#### SetCurrentCultureInvariant()

Sets the culture to invariant for consistent formatting:

```csharp
SetCurrentCultureInvariant();
```

### Available TestSuite Types

Test Fabric provides several pre-configured test suite types:

- **`TestSuite.Normal`**: Standard configuration with randomization
- **`TestSuite.WithRecursion`**: Configured to handle recursive object creation
-

```csharp
// For most scenarios
public class StandardTests : TestSuite.Normal { }

// For complex object graphs with potential recursion
public class ComplexObjectTests : TestSuite.WithRecursion { }
```

### Creating Custom TestSuite Classes

You can create your own custom test suite by implementing a custom data factory builder and configuring the AutoFixture
settings to meet your specific needs.

```csharp
// Step 1: Create a custom data factory builder
public class CustomDataFactoryBuilder : IFactoryBuilder
{
    public IFactory Create()
    {
        var fixture = new Fixture();
        
        // Configure custom behaviors
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior(recursionDepth: 2));
        
        // Add custom specimen builders
        fixture.Customizations.Add(new EmailAddressGenerator());
        fixture.Customizations.Add(new CustomDateTimeGenerator());
        
        // Configure specific types
        fixture.Customize<User>(composer =>
            composer
                .With(u => u.IsActive, true)
                .With(u => u.CreatedDate, DateTime.Now.AddDays(-30))
                .Without(u => u.Password) // Don't generate passwords
        );
        
        return new Factory(fixture, null, new DoublePicker(), new IntPicker());
    }
}

// Step 2: Create your custom test suite
public class CustomTestSuite : TestSuite<CustomDataFactoryBuilder>
{
    // Add any additional helper methods specific to your testing needs
    protected User CreateValidUser()
    {
        return Factory.BuildConstrained<User>()
            .With(u => u.Email, Random<string>() + "@business.com")
            .With(u => u.Age, InRange(18, 120))
            .Create();
    }
    
    protected Product CreateProductInCategory(string category)
    {
        return Factory.BuildConstrained<Product>()
            .With(p => p.Category, category)
            .With(p => p.Price, Factory.CreateFromRange(1d, 1000d))
            .Create();
    }
}

// Step 3: Use your custom test suite
public class AdvancedUserTests : CustomTestSuite
{
    [Fact]
    public void Should_Process_Valid_Users()
    {
        // Arrange - Use your custom helper methods
        var user = CreateValidUser();
        var service = new UserService();

        // Act
        var result = service.ProcessUser(user);

        // Assert
        Assert.True(result.Success);
        Assert.True(user.IsActive);
        Assert.True(user.CreatedDate < DateTime.Now);
    }
    
    [Fact]
    public void Should_Handle_Electronics_Category()
    {
        // Arrange
        var product = CreateProductInCategory("Electronics");
        var service = new ProductService();

        // Act
        var result = service.CategorizeProduct(product);

        // Assert
        Assert.Equal("Electronics", result.Category);
        Assert.True(1d <= product.Price);
        Assert.True(product.Price < 1000d);
    }
}
```

### Best Practices

1. **Use TestSuite.Normal for most scenarios**: It provides a good balance of randomization and performance.
2. **Constrain your data appropriately**: Use `BuildConstrained<T>()` to ensure generated data meets your business
   rules.
3. **Leverage randomization**: Let the data factory generate varied test data to help discover edge cases.
4. **Create domain-specific test suites**: For complex domains, create custom test suites with domain-specific helper
   methods.
5. **Configure recursion handling**: Use `TestSuite.WithRecursion` or custom builders when dealing with complex object
   graphs.