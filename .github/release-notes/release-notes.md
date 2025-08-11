Provides easier ways to generate randomized data.

## 🛠 Enhancements

- generate recent dates based on a timespan.
- add `Random<T>(int count)` to generate multiple data objects.
- add `InRange<T>(int count, T min, T max)` and `InRange<T>(int count, IEnumerable<T> items)` to generate multiple data
  objects.
- add a decimal picker and add it as a default picker.
- add a `TestClock`.
- use UtcNow for the determination of a recent date.

## 📦 NuGet

Package available at: [NuGet.org](https://www.nuget.org/packages/TestFabric)

```
dotnet add package TestFabric --version 0.3.3
```
