namespace TestFabric.Data;

/// <summary>
///     A class for picking a double from a specified range.
///     Inherits from <see cref="RangePicker{T, TRange}" />.
/// </summary>
public class DoublePicker : RangePicker<double, NumberRange<double>>
{
    /// <inheritdoc />
    protected override double PickFromRange(NumberRange<double> range)
    {
        return (range.End - range.Start) * Random.NextDouble() + range.Start;
    }
}
