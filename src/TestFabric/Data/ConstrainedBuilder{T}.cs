namespace TestFabric.Data;

/// <summary>
///     Represents a builder for creating constrained instances of a specific type.
///     This builder allows adding options and ranges but does not support setting properties or defaulting them.
/// </summary>
/// <typeparam name="T">The type of the object to build.</typeparam>
public class ConstrainedBuilder<T>(
    IPicker<T> rangePicker,
    int? seed = null)
    : IConstrainedBuilder<T>
{
    private readonly IPicker<T> _itemPicker = new ItemPicker<T>();
    private readonly List<T> _options = [];
    private readonly Random _random = seed.ToRandom();
    private readonly List<IRange<T>> _ranges = [];

    /// <inheritdoc />
    public T Create()
    {
        if (_options.Count == 0 && _ranges.Count == 0)
        {
            throw new InvalidOperationException(
                "you need at least one option or range");
        }

        var selection = _ranges
            .Select(rangePicker.Pick)
            .ToList();
        if (_options.Count > 0)
        {
            selection.Add(_itemPicker.Pick(new ItemRange<T>(_options)));
        }

        return _itemPicker.Pick(new ItemRange<T>(selection));
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany()
    {
        return CreateMany(3, 10);
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => Create())
            .ToList(); // sequence must be evaluated
    }

    /// <inheritdoc />
    public IEnumerable<T> CreateMany(int countMin, int countMax)
    {
        var count = _random.Next(countMin, countMax + 1);
        return CreateMany(count);
    }

    /// <inheritdoc />
    public IConstrainedBuilder<T> AddOptions(params T[] options)
    {
        if (options == null || options.Length == 0)
        {
            return this;
        }

        _options.AddRange(options);
        return this;
    }

    /// <inheritdoc />
    public IConstrainedBuilder<T> AddRange<TRange>(TRange range) where TRange : IRange<T>
    {
        if (range == null)
        {
            return this;
        }

        _ranges.Add(range);
        return this;
    }
}
