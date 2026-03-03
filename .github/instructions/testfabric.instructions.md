---
applyTo: "tests/**/*.cs"
---

# TestFabric Library — Agent Instructions

This project uses the **TestFabric** NuGet package (`using TestFabric;`) for unit and integration testing.
TestFabric provides fluent mock builders (over Moq), test data generation (over AutoFixture), equality comparers, and a
test clock.
Follow these instructions precisely when generating, modifying, or reviewing test code.

---

## 1. Test Class Base Class

Every test class MUST inherit from `TestSuite.Normal` (or `TestSuite.WithRecursion` for recursive object graphs).
Do NOT create a `Fixture` or `AutoFixture` instance directly. The base class exposes a static `Factory` property and
helper methods.

```csharp
using TestFabric;

public class OrderServiceTests : TestSuite.Normal
{
    // Factory, Random<T>(), InRange<T>(), FirstName(), etc. are available here.
}
```

Use `TestSuite.WithRecursion` only when the object graph contains self-referencing types.

---

## 2. Generating Test Data

### 2.1 Random Objects

Use the inherited `Random<T>()` method (not `Factory.Create<T>()` directly, though both work).

```csharp
var person = Random<Person>();           // single random object
var people = Random<Person>(5);          // 5 random objects
var id = Random<Guid>();
var name = Random<string>();
```

### 2.2 Ranged / Constrained Values

Use `InRange<T>(min, max)` for numeric, date, and timespan ranges. Min is inclusive, max is exclusive.

```csharp
var age = InRange(18, 65);                          // int
var price = InRange(10.0, 100.0);                   // double
var ratio = InRange(0.1f, 1.0f);                    // float
var id = InRange(1000L, 9999L);                     // long
var date = InRange(DateTime.Now.AddDays(-30), DateTime.Now);            // DateTime
var offset = InRange(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now); // DateTimeOffset
var duration = InRange(TimeSpan.FromMinutes(1), TimeSpan.FromHours(2));   // TimeSpan
```

To pick from a discrete set:

```csharp
var color = InRange(new[] { "Red", "Green", "Blue" });
var colors = InRange(5, new[] { "Red", "Green", "Blue" }); // 5 picks, may repeat
```

Multiple ranged values:

```csharp
var ages = InRange(5, 18, 65); // 5 ints between 18–64
```

### 2.3 Factory Builder API

For fine-grained control, use `Factory.Build<T>()` or `Factory.BuildConstrained<T>()`:

```csharp
var product = Factory.Build<Product>()
    .With(p => p.Price, Factory.CreateFromRange(10.0, 100.0))
    .Create();

var validAge = Factory.BuildConstrained<int>()
    .AddOptions(2, 7, 13)
    .AddRange(new NumberRange<int>(20, 30))
    .Create();
```

### 2.4 Personal / Fake Data Helpers

Use the built-in helpers instead of inventing fake data:

```csharp
var first = FirstName();        // e.g. "Maria"
var last = LastName();          // e.g. "Garcia"
var full = FullName();          // e.g. "Maria Garcia"
var latin = FullName(hasSecondLastName: true); // e.g. "Maria Garcia Lopez"
var email = Email();            // e.g. "maria.garcia@example.com"
var company = CompanyName();    // e.g. "NimbusWorks"
var country = Country();        // e.g. "Germany"
var city = City("Germany");     // e.g. "Berlin"
```

### 2.5 Date/Time Helpers

```csharp
var recent = RecentDateTime();                  // random DateTime within last 30 days
var lastWeek = RecentDateTime(7);               // within last 7 days
var lastHour = RecentDateTime(TimeSpan.FromHours(1));
var recentOffset = RecentDateTimeOffset();      // DateTimeOffset within last 30 days
var offsetLastWeek = RecentDateTimeOffset(TimeSpan.FromDays(7));
```

### 2.6 Template-Based Generation

Create objects from a template, randomizing only specified properties:

```csharp
var template = new User { Name = "Template", IsActive = true };
// Randomize Name and Email; keep IsActive = true from template
var user = FromTemplate(template, x => x.Name, x => x.Email);
var users = FromTemplate(5, template, x => x.Name, x => x.Email);
```

### 2.7 Culture Control

```csharp
SetCurrentCulture("es-ES");
SetCurrentCulture(CultureInfo.GetCultureInfo("de-DE"));
SetCurrentCultureInvariant();
```

---

## 3. MockBuilder Pattern

### 3.1 Creating a MockBuilder

For every interface that needs mocking, create a dedicated builder class inheriting `MockBuilder<T>`.
The mock uses `MockBehavior.Strict` by default.

Naming convention: `{InterfaceName}MockBuilder` (drop the leading `I`).
Place mock builders in a shared test tools project (e.g., `*.TestTools`) so they are reusable across test projects.

```csharp
using TestFabric;

public class UserRepositoryMockBuilder : MockBuilder<IUserRepository>
{
    public UserRepositoryMockBuilder WithGetById(User user)
    {
        WithFunction(x => x.GetById(It.IsAny<int>()), user);
        return this;
    }

    public UserRepositoryMockBuilder WithSave()
    {
        WithAction(x => x.Save(It.IsAny<User>()));
        return this;
    }
}
```

Rules:

- Each `With*` method configures ONE mock behavior and returns `this` for fluent chaining.
- Use `It.IsAny<T>()` for parameter matchers (from Moq, globally available).
- Call `Create()` to get the `Mock<T>`. Access the object via `.Object`.

### 3.2 Available MockBuilder Helper Methods

#### Properties

```csharp
// Read/Write property (get + set)
WithProperty(x => x.Name, "Initial Value");

// Get-only property returning a fixed value
WithPropertyGet(x => x.Id, 42);

// Get-only property returning a computed value
WithPropertyGet(x => x.Timestamp, () => DateTime.Now);
```

#### Actions (void methods)

```csharp
// No callback
WithAction(x => x.DoSomething());

// With typed callback (0–5 argument overloads exist)
WithAction<string>(x => x.Process(It.IsAny<string>()), value => Console.WriteLine(value));
WithAction<string, int>(x => x.Process(It.IsAny<string>(), It.IsAny<int>()), (s, i) => { });
```

#### Async Actions (methods returning Task)

```csharp
WithActionAsync(x => x.ProcessAsync());
WithActionAsync<int>(x => x.ProcessAsync(It.IsAny<int>()), id => Console.WriteLine(id));
```

#### Functions (methods with return values)

```csharp
// Fixed return value
WithFunction(x => x.Calculate(), 100);

// Sequential return values
WithFunction(x => x.GetNext(), 1, 2, 3, 4);

// Delegate return (0–5 argument overloads exist)
WithFunction(x => x.GetValue(), () => computedValue);
WithFunction<string, int>(x => x.Parse(It.IsAny<string>()), s => int.Parse(s));
WithFunction<int, string, bool>(x => x.Validate(It.IsAny<int>(), It.IsAny<string>()), (i, s) => true);
```

#### Async Functions (methods returning Task<T>)

```csharp
// Fixed async return
WithFunctionAsync(x => x.ComputeAsync(), 42);

// Sequential async returns
WithFunctionAsync(x => x.GetNextAsync(), 1, 2, 3);

// Delegate async return (0–5 argument overloads exist)
WithFunctionAsync(x => x.ComputeAsync(), () => 42);
WithFunctionAsync<string, int>(x => x.ParseAsync(It.IsAny<string>()), s => int.Parse(s));
```

#### TryGet Pattern (bool return with out parameter)

```csharp
// Fixed out value
WithTryGet(x => x.TryGetValue(out outValue), true, outValue);

// With input argument(s) — generic type params specify input types
WithTryGet<string, int>(x => x.TryParse(It.IsAny<string>(), out outValue), true, outValue);
```

### 3.3 Using a MockBuilder in Tests

```csharp
[Fact]
public void Should_Save_User()
{
    // Arrange
    var user = Random<User>();
    var mockRepository = new UserRepositoryMockBuilder()
        .WithGetById(user)
        .WithSave()
        .Create();
    var sut = new UserService(mockRepository.Object);

    // Act
    sut.UpdateUser(user.Id, "Jane");

    // Assert
    mockRepository.Verify(x => x.Save(It.IsAny<User>()), Times.Once);
}
```

### 3.4 Built-in MockBuilders

TestFabric ships these ready-to-use mock builders. Do NOT recreate them.

#### LoggerMockBuilder<T> — mocks ILogger<T>

```csharp
// Silently accept all log calls
var mockLogger = new LoggerMockBuilder<MyService>().WithLog().Create();

// Capture log messages into a list
var logMessages = new List<string>();
var mockLogger = new LoggerMockBuilder<MyService>().WithLog(logMessages).Create();
// Messages are formatted as "[LogLevel] message text"

// Custom handler
var mockLogger = new LoggerMockBuilder<MyService>()
    .WithLog((level, text, ex) => { /* custom logic */ })
    .Create();
```

#### ProgressMockBuilder<T> — mocks IProgress<T>

```csharp
// Silently accept reports
var mockProgress = new ProgressMockBuilder<string>().WithReport().Create();

// Capture reported values
var values = new List<int>();
var mockProgress = new ProgressMockBuilder<int>()
    .WithReport(v => values.Add(v))
    .Create();
```

#### EqualityComparerMockBuilder<T> — mocks IEqualityComparer<T>

```csharp
var mockComparer = new EqualityComparerMockBuilder<User>()
    .WithEquals((u1, u2) => u1.Id == u2.Id)
    .WithGetHashCode(u => u.Id.GetHashCode())
    .Create();
```

---

## 4. TestClock — Controlling Time in Tests

Use `TestClock` for any time-dependent test logic. Do NOT use `DateTime.Now`/`DateTimeOffset.UtcNow` directly in code
under test.

```csharp
var testClock = new TestClock();
testClock.StartAt(new DateTimeOffset(2023, 6, 15, 10, 0, 0, TimeSpan.Zero));

// Use testClock.UtcNow wherever the SUT needs the current time
var timestamp = testClock.UtcNow;

// Advance time
testClock.Advance(TimeSpan.FromMinutes(30));
```

Integrate with mock builders by passing the clock and using a delegate return:

```csharp
public class TimeStampGeneratorMockBuilder : MockBuilder<ITimeStampGenerator>
{
    public TimeStampGeneratorMockBuilder WithGenerate(TestClock clock)
    {
        WithFunction(x => x.Generate(), () => clock.UtcNow);
        return this;
    }
}
```

---

## 5. Equality Comparers

### 5.1 Compare Factory

Use the `Compare` static class to create comparers. Do NOT implement custom double comparers manually.

```csharp
// Absolute tolerance for doubles
IEqualityComparer<double> comparer = Compare.DoubleAbsolute(0.01);

// Relative tolerance for doubles (with absolute tolerance for near-zero)
IEqualityComparer<double> comparer = Compare.DoubleRelative(0.05, 1e-10);

// Array comparer (optionally with element comparer)
IEqualityComparer<double[]> arrayComparer = Compare.Array(Compare.DoubleAbsolute(0.001));

// List comparer (optionally with element comparer)
IEqualityComparer<List<double>> listComparer = Compare.List(Compare.DoubleAbsolute(0.001));
```

### 5.2 ObjectEqualityComparer<T>

Extend this base class for domain-specific equality rules:

```csharp
public class UserIdComparer : ObjectEqualityComparer<User>
{
    protected override bool EqualsImpl(User x, User y)
    {
        return x.Id == y.Id;
    }
}
```

### 5.3 EqualityComparerBuilder<T>

Use the fluent builder for complex comparison logic in tests:

```csharp
var comparer = new EqualityComparerBuilder<Order>()
    .IgnoreMember(x => x.Timestamp)                         // skip specific members
    .IgnoreMember(x => x.Id)
    .ConfigureMember(x => x.Price, Compare.DoubleAbsolute(0.01))  // custom member comparer
    .ConfigureMember(x => x.Name, (a, b) => string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
    .ConfigureType<DateTime>((d1, d2) => d1.Date == d2.Date)      // type-level comparer
    .IgnoreType<AuditInfo>()                                       // ignore entire types
    .EnableTracing(line => traceOutput.Add(line), detailed: true)  // debug comparison failures
    .Create();
```

The builder automatically handles arrays and lists as members.

---

## 6. Assertions

This project uses **AwesomeAssertions** (fluent assertions, `.Should()` API). Always prefer fluent assertions over
xUnit/MSTest assert methods.

```csharp
result.Name.Should().Be(expected.Name);
result.Age.Should().BeGreaterThanOrEqualTo(18);
results.Should().HaveCount(5);
results.Should().ContainSingle();
act.Should().Throw<InvalidOperationException>();
logMessages.Should().Contain(msg => msg.Contains("[Error]"));
```

For approximate double comparison:

```csharp
result.Should().BeApproximately(expected, 0.01);
```

For collection equivalence with custom element comparison:

```csharp
results.Should().BeEquivalentTo(expected, options =>
    options.Using<double>(ctx =>
        ctx.Subject.Should().BeApproximately(ctx.Expectation, 0.001))
    .WhenTypeIs<double>());
```

---

## 7. Test Structure Conventions

### 7.1 Arrange-Act-Assert

Always structure tests with clearly separated Arrange / Act / Assert sections using comments:

```csharp
[Fact]
public void Should_Create_Order_With_Correct_Total()
{
    // Arrange
    var price = InRange(10.0, 100.0);
    var quantity = InRange(1, 20);
    var sut = new OrderCalculator();

    // Act
    var total = sut.CalculateTotal(price, quantity);

    // Assert
    total.Should().BeApproximately(price * quantity, 0.01);
}
```

### 7.2 Test Naming

Use descriptive names: `Given_<Context>_When_<Action>_Then_<Result>`.

### 7.3 SUT Naming

Name the system under test `sut`.

### 7.4 Mock Builder Placement

- Put reusable mock builders in a shared `*.TestTools` project.
- One mock builder per interface.
- Each `With*` method corresponds to one interface member being configured.

### 7.5 Namespace

Use `using TestFabric;` — this imports `MockBuilder<T>`, `TestSuite`, `Compare`, `TestClock`, all builder types, and
re-exports `Moq` (including `It`, `Mock`, `Times`, etc.).

Configure the using in the test project file as a global using directive to avoid adding them at the top of every test
file.

---

## 8. Quick Reference — What NOT To Do

| ❌ Don't                                        | ✅ Do Instead                                                                 |
|------------------------------------------------|------------------------------------------------------------------------------|
| `new Fixture()` or `new AutoFixture.Fixture()` | Inherit `TestSuite.Normal` and use `Factory` / `Random<T>()`                 |
| `new Mock<T>(MockBehavior.Strict)` directly    | Create a `MockBuilder<T>` subclass                                           |
| `DateTime.Now` in test assertions              | Use `TestClock`                                                              |
| `Assert.Equal(a, b)` for doubles               | `a.Should().BeApproximately(b, tolerance)` or use `Compare.DoubleAbsolute()` |
| Manually implement `IEqualityComparer<double>` | Use `Compare.DoubleAbsolute()` / `Compare.DoubleRelative()`                  |
| `Assert.True(...)` / `Assert.False(...)`       | `value.Should().BeTrue()` / `value.Should().BeFalse()`                       |
| Hardcoded test data everywhere                 | `Random<T>()`, `InRange()`, `FirstName()`, `Email()`, etc.                   |
| Duplicating mock builder classes               | Share via a `*.TestTools` project                                            |
