namespace TestFabric.Coder.Extractor;

internal static class Enrich
{
    internal static Dictionary<string, Example[]> CreateExamples()
    {
        // Keys must match the full class name as it appears in tools/TestFabric.Coder.Mcp/index.json (Symbol field)
        // For generic types, that means using the backtick arity suffix, e.g., "TestFabric.MockBuilder`1".

        var result = new Dictionary<string, Example[]>
        {
            ["TestFabric.MockBuilder`1"] =
            [
                new Example(
                    "Fluent MockBuilder with property setup",
                    """
                    using TestFabric;

                    public interface IFoo
                    {
                        int Id { get; }
                        double Value { get; set; }
                    }

                    public sealed class FooMockBuilder : MockBuilder<IFoo>
                    {
                        public FooMockBuilder WithId(int id)
                        {
                            // Configure getter via helper
                            WithPropertyGet(f => f.Id, id);
                            return this;
                        }

                        public FooMockBuilder WithValue(double initial)
                        {
                            // Backing property with initial value (can be mutated in test)
                            WithProperty(f => f.Value, initial);
                            return this;
                        }
                    }

                    // Usage in a test
                    var foo = new FooMockBuilder()
                        .WithId(42)
                        .WithValue(0.0)
                        .Create().Object;
                    """,
                    "Demonstrates fluent WithX pattern and property helpers (WithPropertyGet/WithProperty)."),
                new Example(
                    "Async method setup with WithFunctionAsync",
                    """
                    using TestFabric;
                    using System;
                    using System.Threading.Tasks;

                    public interface IRepository
                    {
                        Task<Entity?> GetByIdAsync(Guid id);
                    }

                    public sealed record Entity(Guid Id);

                    public sealed class RepositoryMockBuilder : MockBuilder<IRepository>
                    {
                        public RepositoryMockBuilder WithGetById(Func<Guid, Task<Entity?>> func) =>
                            this.WithFunctionAsync(r => r.GetByIdAsync(default), func);
                    }

                    // Usage in a test
                    var expected = new Entity(Guid.NewGuid());
                    var repo = new RepositoryMockBuilder()
                        .WithGetById(id => Task.FromResult<Entity?>(expected))
                        .Create().Object;
                    """,
                    "Uses expression placeholder (default) for parameters and provides async delegate.")
            ],
            ["TestFabric.EqualityComparerMockBuilder`1"] =
            [
                new Example(
                    "Build custom IEqualityComparer<T> with fluent configuration",
                    """
                    using System.Collections.Generic;
                    using TestFabric;

                    public sealed record Person(int Id, string Name);

                    var comparer = new EqualityComparerMockBuilder<Person>()
                        .WithEquals((x, y) => x.Id == y.Id)
                        .WithGetHashCode(p => p.Id.GetHashCode())
                        .Create().Object;

                    var a = new Person(1, "Ann");
                    var b = new Person(1, "Anne");
                    EqualityComparer<Person>.Default.Equals(a, b); // false
                    comparer.Equals(a, b); // true (by Id)
                    """,
                    "Shows overriding Equals and GetHashCode via the builder.")
            ],
            ["TestFabric.LoggerMockBuilder`1"] =
            [
                new Example(
                    "Capture ILogger<T> calls with WithLog",
                    """
                    using System;
                    using System.Collections.Generic;
                    using Microsoft.Extensions.Logging;
                    using TestFabric;

                    public sealed class MyService;

                    var logs = new List<(LogLevel level, EventId eventId, string message)>();

                    var logger = new LoggerMockBuilder<MyService>()
                        .WithLog(logs)
                        .Create();

                    // Example usage in SUT
                    logger.Object.LogInformation("Hello {Name}", "World");

                    // Assert on collected logs (level/message)
                    // logs[0].level == LogLevel.Information; logs[0].message contains "Hello World"
                    """,
                    "Demonstrates log capture for assertions in tests.")
            ],
            ["TestFabric.ProgressMockBuilder`1"] =
            [
                new Example(
                    "Assert IProgress<T>.Report calls",
                    """
                    using System.Collections.Generic;
                    using TestFabric;

                    var updates = new List<int>();
                    var progress = new ProgressMockBuilder<int>()
                        .WithReport(updates)
                        .Create().Object;

                    // SUT
                    progress.Report(1);
                    progress.Report(2);

                    // Assert
                    // updates == [1, 2]
                    """,
                    "Collects reported values for later assertions.")
            ]
        };

        return result;
    }
}
