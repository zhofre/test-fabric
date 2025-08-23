namespace TestFabric.Data;

/// <summary>
///     Picker for <see cref="TimeSpan" /> values.
/// </summary>
public class TimeSpanPicker : RangePicker<TimeSpan, NumberRange<TimeSpan>>
{
    /// <inheritdoc />
    protected override TimeSpan PickFromRange(NumberRange<TimeSpan> range)
    {
        var totalTicks = range.End.Ticks - range.Start.Ticks;
        if (totalTicks <= 0)
        {
            return range.Start;
        }

        var offsetTicks = (long)(Random.NextDouble() * totalTicks);
        return range.Start.Add(TimeSpan.FromTicks(offsetTicks));
    }
}
