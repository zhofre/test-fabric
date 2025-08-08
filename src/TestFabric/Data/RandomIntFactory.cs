namespace TestFabric.Data;

/// <summary>
///     Factory class for generating random integers within specified bounds.
/// </summary>
public class RandomIntFactory(int? seed = null)
    : RandomNumberFactory<int>(0, Epsilon, SmallBound, MediumBound, LargeBound, seed)
{
    internal const int Epsilon = 1;
    internal const int SmallBound = 10;
    internal const int MediumBound = 1000;
    internal const int LargeBound = 100000;

    /// <inheritdoc />
    protected override int Create(int lowerBound, int upperBound)
    {
        return CreateSign() * CreatePositive(lowerBound, upperBound);
    }

    /// <inheritdoc />
    protected override int CreatePositive(int lowerBound, int upperBound)
    {
        return CreateRandom(lowerBound, upperBound);
    }
}
