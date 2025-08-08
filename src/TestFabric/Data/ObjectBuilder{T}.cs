using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;

namespace TestFabric.Data;

/// <summary>
///     Class for building instances of a specific type T.
///     Provides methods to configure and generate objects using AutoFixture.
/// </summary>
/// <typeparam name="T">The type of object to build.</typeparam>
public class ObjectBuilder<T>(
    IPostprocessComposer<T> composer,
    int? seed = null)
    : IObjectBuilder<T>
{
    private readonly Random _random = seed.ToRandom();
    private IPostprocessComposer<T> _composer = composer;

    /// <inheritdoc />
    public IObjectBuilder<T> With<TProperty>(
        Expression<Func<T, TProperty>> propertyPicker,
        TProperty value)
    {
        _composer = _composer.With(propertyPicker, value);
        return this;
    }

    /// <inheritdoc />
    public IObjectBuilder<T> Without<TProperty>(
        Expression<Func<T, TProperty>> propertyPicker)
    {
        _composer = _composer.Without(propertyPicker);
        return this;
    }

    /// <inheritdoc />
    public T Create()
    {
        ISpecimenBuilder builder = _composer;
        return new SpecimenContext(builder)
            .Create<T>();
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany()
    {
        return CreateMany(3, 10);
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany(int count)
    {
        var builder = (ISpecimenBuilder)_composer;
        return new SpecimenContext(builder)
            .CreateMany<T>(count);
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany(int countMin, int countMax)
    {
        var count = _random.Next(countMin, countMax + 1);
        return CreateMany(count);
    }
}
