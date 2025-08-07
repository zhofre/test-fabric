namespace TestFabric;

/// <summary>
///     Compares two double values based on their absolute difference within a specified precision.
///     Handles special cases such as NaN and infinity.
/// </summary>
/// <param name="precision">the precision used for the comparison</param>
public class AbsoluteDoubleComparer(double precision = 1e-6) : IEqualityComparer<double>
{
    /// <inheritdoc />
    public bool Equals(double x, double y)
    {
        // Handle NaN values - NaN is only equal to NaN
        if (double.IsNaN(x) || double.IsNaN(y))
        {
            return double.IsNaN(x) && double.IsNaN(y);
        }

        // Handle infinity values - use exact equality for infinities
        if (double.IsInfinity(x) || double.IsInfinity(y))
        {
            return x.Equals(y);
        }

        // Standard absolute difference comparison for normal values
        return Math.Abs(x - y) < precision;
    }

    /// <inheritdoc />
    public int GetHashCode(double obj)
    {
        // Ensure consistent hash codes for special values
        if (double.IsNaN(obj))
        {
            return 0; // All NaN values should have the same hash code
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
