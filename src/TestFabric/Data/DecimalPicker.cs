namespace TestFabric.Data;

/// <summary>
///     A class for picking a decimal from a specified range.
///     Inherits from <see cref="RangePicker{T, TRange}" />.
/// </summary>
public class DecimalPicker : RangePicker<decimal, NumberRange<decimal>>
{
    /// <inheritdoc />
    protected override decimal PickFromRange(NumberRange<decimal> range)
    {
        return (range.End - range.Start) * (decimal)Random.NextDouble() + range.Start;
    }
}
