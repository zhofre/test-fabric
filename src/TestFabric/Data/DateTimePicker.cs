namespace TestFabric.Data;

/// <summary>
///     A class for picking an <see cref="DateTime" /> from a specified range.
///     Inherits from <see cref="RangePicker{T, TRange}" />.
/// </summary>
public class DateTimePicker : RangePicker<DateTime, NumberRange<DateTime>>
{
    /// <inheritdoc />
    protected override DateTime PickFromRange(NumberRange<DateTime> range)
    {
        var totalTicks = (range.End - range.Start).Ticks;
        var randomTicks = (long)(Random.NextDouble() * totalTicks);
        return range.Start.AddTicks(randomTicks);
    }
}
