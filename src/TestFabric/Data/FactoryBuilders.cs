using AutoFixture;

namespace TestFabric.Data;

/// <summary>
///     Factory builders for creating data instances with different configurations.
/// </summary>
public static class FactoryBuilders
{
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
            return new Factory(fixture, null, new DoublePicker(), new IntPicker());
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
            return new Factory(fixture, null, new DoublePicker(), new IntPicker());
        }
    }
}
