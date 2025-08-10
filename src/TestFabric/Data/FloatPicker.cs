namespace TestFabric.Data;

/// <summary>
///     A class for picking a float from a specified range.
///     Inherits from <see cref="RangePicker{T, TRange}" />.
/// </summary>
public class FloatPicker : RangePicker<float, NumberRange<float>>
{
    /// <inheritdoc />
    protected override float PickFromRange(NumberRange<float> range)
    {
        return (range.End - range.Start) * (float)Random.NextDouble() + range.Start;
    }
}
