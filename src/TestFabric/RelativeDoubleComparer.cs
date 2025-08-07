namespace TestFabric;

public class RelativeDoubleComparer(double relativeTolerance = 1e-3, double absoluteTolerance = 1e-30)
    : IEqualityComparer<double>
{
    /// <inheritdoc />
    public bool Equals(double x, double y)
    {
        if (double.IsNaN(x) || double.IsNaN(y))
        {
            return double.IsNaN(x) && double.IsNaN(y);
        }

        if (double.IsInfinity(x) || double.IsInfinity(y))
        {
            return x.Equals(y);
        }

        var diff = Math.Abs(x - y);
        var maxAbs = Math.Max(Math.Abs(x), Math.Abs(y));

        if (maxAbs < absoluteTolerance)
        {
            return diff <= absoluteTolerance;
        }

        return diff <= relativeTolerance * maxAbs;
    }

    /// <inheritdoc />
    public int GetHashCode(double obj)
    {
        // this is a non-performant implementation to make sure HashSet/Dictionary work correctly
        if (double.IsNaN(obj) || Math.Abs(obj) < absoluteTolerance)
        {
            return 0;
        }

        if (double.IsInfinity(obj))
        {
            return obj > 0 ? int.MaxValue : int.MinValue; // Distinguish +∞ from -∞
        }

        // Forces hash collision because there is no way to consistently put different
        // values in buckets. Values that are close enough to be considered equal
        // could end up in different buckets.
        return 1;
    }
}
