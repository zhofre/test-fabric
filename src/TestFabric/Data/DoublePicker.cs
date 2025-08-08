namespace TestFabric.Data;

/// <summary>
///     A class for picking a double from a specified range.
///     Inherits from RangePicker&lt;double, NumberRange&lt;double&gt;&gt;.
/// </summary>
public class DoublePicker : RangePicker<double, NumberRange<double>>
{
    /// <inheritdoc />
    protected override double PickFromRange(NumberRange<double> range)
    {
        return (range.End - range.Start) * Random.NextDouble() + range.Start;
    }
}
