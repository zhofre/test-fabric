# Project Guidelines

## Tech & Architecture

- .NET Standard 2.0 class libraries

## Coding Standards

### General

- Follow .editorconfig standards
- Use C# 12+ features: primary constructors, collection expressions `[]`, `Lock` type for thread safety
- Method parameters should be formatted with each parameter on its own line when there are multiple parameters
- Use nullable reference types consistently (`?` for nullable, non-null by default)

### Naming Conventions

- DTOs and contracts use suffixes: `Request`, `Response`, `Result`, `Dto`
- Workflow classes are named `<Feature>Workflows` (e.g., `AdminWorkflows`, `AuthWorkflows`)
- Repository interfaces are named `I<Entity>Repository` (e.g., `ILicenseRepository`)
- Test classes are named `<ClassUnderTest>Tests` and should use `[TestSubject(typeof(T))]` attribute
- Constants use PascalCase

### Code Organization

- Use records for DTOs with positional parameters
- Use `IReadOnlyList<T>` or `IEnumerable<T>` for collection parameters in public APIs
- Converters should be sealed classes inheriting from `JsonConverter<T>`
- Helper methods in classes should be private and static when they don't need instance state
- Group related endpoints using private static helper methods (e.g., `ConfigureAuthEndpoints`, `ConfigureAdminEndpoints`)

## Testing

- All test names should be named Given\_&lt;assumptions&gt;\_When\_&lt;action&gt;\_Then\_<result>
- Test classes should inherit from `TestSuite.Normal` base class
- Use Arrange/Act/Assert comments to structure test methods
- Services that are injected as an interface should be defined as a `MockBuilder<T>` for use in unit tests
- `MockBuilder<T>` should use fluent API pattern with `WithX` methods returning `this` for chaining
- Use `WithFunctionAsync` to set up async method behaviors in MockBuilder
- Any ILogger is injected as a `Mock<ILogger>` for use in unit tests and be Loose
- Any ILogger<T> is injected as a `Mock<ILogger<T>>` for use in unit tests and be Loose
- Use `Mock<T>(MockBehavior.Strict)` for precise behavior verification, `MockBehavior.Loose` for lenient tests
- For random data, use TestFabric's Random<> or InRange functionality
- Create helper methods for test data (e.g., `L(...)` for creating license DTOs) when appropriate
- Use collection expressions for expected values in assertions (e.g., `Assert.Equal([...], actual)`)