namespace TestFabric.Data;

/// <summary>
///     Factory class for generating random doubles within specified bounds.
/// </summary>
public class RandomDoubleFactory(int? seed = null)
    : RandomNumberFactory<double>(SmallestBound, SmallBound, NormalBound, LargeBound, seed)
{
    internal const double SmallestBound = 1e-9;
    internal const double SmallBound = 1d;
    internal const double NormalBound = 100d;
    internal const double LargeBound = 1.0e6;

    private const int RandomBound = 1000000;
    private const double Multiplier = 1.0e-6;

    /// <inheritdoc />
    protected override double Create(double lowerBound, double upperBound)
    {
        return CreateSign() * CreatePositive(lowerBound, upperBound);
    }

    /// <inheritdoc />
    protected override double CreatePositive(double lowerBound, double upperBound)
    {
        var rand = CreateRandom(0, RandomBound) * Multiplier; // [0,1[
        return rand * (upperBound - lowerBound) + lowerBound;
    }

    /// <inheritdoc />
    protected override double BoundToSmallestPositive(double lowerBound)
    {
        return Math.Max(SmallestBound, lowerBound);
    }
}
