namespace TestFabric.Data;

/// <summary>
///     Factory class for generating random integers within specified bounds.
/// </summary>
public class RandomIntFactory(int? seed = null)
    : RandomNumberFactory<int>(SmallestBound, SmallBound, NormalBound, LargeBound, seed)
{
    internal const int SmallestBound = 1;
    internal const int SmallBound = 10;
    internal const int NormalBound = 1000;
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

    /// <inheritdoc />
    protected override int BoundToSmallestPositive(int lowerBound)
    {
        return Math.Max(SmallestBound, lowerBound);
    }
}
