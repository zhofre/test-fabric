using AutoFixture;

namespace TestFabric.Data;

/// <summary>
///     Factory class responsible for creating objects using the AutoFixture library.
/// </summary>
/// <param name="fixture">The AutoFixture fixture used to generate objects.</param>
/// <param name="seed">Optional seed for random number generation.</param>
/// <param name="pickers">Array of IPicker implementations to use for constrained object creation.</param>
public class Factory(
    Fixture fixture,
    int? seed,
    params IPicker[] pickers)
    : IFactory
{
    private readonly Fixture _fixture = fixture
                                        ?? throw new ArgumentNullException(nameof(fixture));

    private readonly Random _random = seed.ToRandom();

    /// <inheritdoc />
    public T Create<T>()
    {
        try
        {
            return _fixture.Create<T>();
        }
        catch (Exception ex)
        {
            throw new FactoryException(
                nameof(Create),
                ex);
        }
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany<T>()
    {
        return CreateMany<T>(3, 10);
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany<T>(int count)
    {
        try
        {
            return _fixture.CreateMany<T>(count);
        }
        catch (Exception ex)
        {
            throw new FactoryException(
                nameof(CreateMany),
                ex);
        }
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany<T>(int countMin, int countMax)
    {
        var count = _random.Next(countMin, countMax + 1);
        return CreateMany<T>(count);
    }

    /// <inheritdoc />
    public T CreateFromRange<T>(T minInclusive, T maxExclusive) where T : IComparable<T>
    {
        return CreateConstrained(minInclusive, maxExclusive)
            .Create();
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateManyFromRange<T>(T minInclusive, T maxExclusive) where T : IComparable<T>
    {
        return CreateConstrained(minInclusive, maxExclusive)
            .CreateMany();
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateManyFromRange<T>(int count, T minInclusive, T maxExclusive) where T : IComparable<T>
    {
        return CreateConstrained(minInclusive, maxExclusive)
            .CreateMany(count);
    }

    /// <inheritdoc />
    public T CreateFromRange<T>(IEnumerable<T> items)
    {
        return CreateConstrained(items)
            .Create();
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateManyFromRange<T>(IEnumerable<T> items)
    {
        return CreateConstrained(items)
            .CreateMany();
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateManyFromRange<T>(int count, IEnumerable<T> items)
    {
        return CreateConstrained(items)
            .CreateMany(count);
    }

    /// <inheritdoc />
    public IBuilder<T> Build<T>()
    {
        return new Builder<T>(_fixture.Build<T>());
    }

    /// <inheritdoc />
    public IConstrainedBuilder<T> BuildConstrained<T>()
    {
        if (pickers == null || pickers.Length == 0)
        {
            throw new InvalidOperationException(
                "no pickers provided");
        }

        var picker = pickers
            .OfType<IPicker<T>>()
            .FirstOrDefault();

        return picker == null
            ? throw new InvalidOperationException(
                $"no range picker matches type {typeof(T).Name}")
            : new ConstrainedBuilder<T>(picker);
    }

    /// <inheritdoc />
    public IConstrainedBuilder<T> BuildConstrainedFromRange<T>(T minInclusive, T maxExclusive) where T : IComparable<T>
    {
        return CreateConstrained(minInclusive, maxExclusive);
    }

    /// <inheritdoc />
    public IConstrainedBuilder<T> BuildConstrainedFromRange<T>(IEnumerable<T> items)
    {
        return CreateConstrained(items);
    }

    private IConstrainedBuilder<T> CreateConstrained<T>(T minInclusive, T maxExclusive)
        where T : IComparable<T>
    {
        return BuildConstrained<T>()
            .AddRange(
                new NumberRange<T>(
                    minInclusive,
                    maxExclusive));
    }

    private IConstrainedBuilder<T> CreateConstrained<T>(IEnumerable<T> items)
    {
        return BuildConstrained<T>()
            .AddOptions(items.ToArray());
    }
}
