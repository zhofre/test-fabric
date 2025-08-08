namespace TestFabric.Data;

/// <summary>
///     A Class for picking a random item from a specified range.
///     Inherits from abstract class RangePicker and implements IPicker interface.
/// </summary>
/// <typeparam name="T">The type of the items within the range.</typeparam>
public class ItemPicker<T> : RangePicker<T, ItemRange<T>>
{
    /// <inheritdoc />
    protected override T PickFromRange(ItemRange<T> range)
    {
        var options = range.Items;
        if (options.Length == 0)
        {
            throw new ArgumentException(
                "range can't be empty",
                nameof(range));
        }

        var index = Random.Next(0, options.Length);
        return options[index];
    }
}
