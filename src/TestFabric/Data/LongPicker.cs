namespace TestFabric.Data;

/// <summary>
///     A class for picking an long integer from a specified range.
///     Inherits from <see cref="RangePicker{T, TRange}" />.
/// </summary>
public class LongPicker : RangePicker<long, NumberRange<long>>
{
    /// <inheritdoc />
    protected override long PickFromRange(NumberRange<long> range)
    {
        return range.Start + (long)(Random.NextDouble() * (range.End - range.Start));
    }
}
