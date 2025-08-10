namespace TestFabric.Data;

/// <summary>
///     A class for picking an <see cref="DateTimeOffset" /> from a specified range.
///     Inherits from <see cref="RangePicker{T, TRange}" />.
/// </summary>
public class DateTimeOffsetPicker : RangePicker<DateTimeOffset, NumberRange<DateTimeOffset>>
{
    /// <inheritdoc />
    protected override DateTimeOffset PickFromRange(NumberRange<DateTimeOffset> range)
    {
        var totalTicks = (range.End - range.Start).Ticks;
        var randomTicks = (long)(Random.NextDouble() * totalTicks);
        return range.Start.AddTicks(randomTicks);
    }
}
