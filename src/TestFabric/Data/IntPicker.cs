namespace TestFabric.Data;

/// <summary>
///     A class for picking an integer from a specified range.
///     Inherits from <see cref="RangePicker{T, TRange}" />.
/// </summary>
public class IntPicker : RangePicker<int, NumberRange<int>>
{
    /// <inheritdoc />
    protected override int PickFromRange(NumberRange<int> range)
    {
        return Random.Next(range.Start, range.End);
    }
}
