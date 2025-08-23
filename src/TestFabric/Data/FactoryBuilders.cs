using AutoFixture;

// ReSharper disable MemberCanBePrivate.Global

namespace TestFabric.Data;

/// <summary>
///     Factory builders for creating data instances with different configurations.
/// </summary>
public static class FactoryBuilders
{
    /// <summary>
    ///     A collection of default pickers implementing the <see cref="IPicker" /> interface.
    ///     The default pickers include support for various data types such as:
    ///     <list type="bullet">
    ///         <item>
    ///             <description><see cref="DateTimePicker" /> for selecting a DateTime value.</description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="DateTimeOffsetPicker" /> for selecting a DateTimeOffset value.</description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="DecimalPicker" /> for selecting a decimal value.</description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="DoublePicker" /> for selecting a double value.</description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="FloatPicker" /> for selecting a float value.</description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="IntPicker" /> for selecting an integer value.</description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="LongPicker" /> for selecting a long value.</description>
    ///         </item>
    ///     </list>
    ///     These pickers provide capabilities for generating random or predefined values within a specific range.
    /// </summary>
    public static readonly IPicker[] DefaultPickers =
    [
        new DateTimePicker(),
        new DateTimeOffsetPicker(),
        new DecimalPicker(),
        new DoublePicker(),
        new FloatPicker(),
        new IntPicker(),
        new LongPicker(),
        new TimeSpanPicker()
    ];

    /// <summary>
    ///     Factory builder for creating normal data instances.
    /// </summary>
    public class Normal : IFactoryBuilder
    {
        /// <inheritdoc />
        public IFactory Create()
        {
            var fixture = new Fixture()
                .RegisterNumberFactories();
            return new Factory(fixture, null, DefaultPickers);
        }
    }

    /// <summary>
    ///     Factory builder for creating data instances with recursion enabled.
    /// </summary>
    public class WithRecursion : IFactoryBuilder
    {
        /// <inheritdoc />
        public IFactory Create()
        {
            var fixture = new Fixture()
                .RegisterNumberFactories()
                .AllowRecursion();
            return new Factory(fixture, null, DefaultPickers);
        }
    }
}
