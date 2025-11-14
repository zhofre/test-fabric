namespace TestFabric;

/// Compares two double-precision floating-point numbers for equality within a specified relative and absolute tolerance.
/// This comparer is designed to handle the challenges of floating-point arithmetic, including rounding errors and
/// precision loss. It uses relative and absolute tolerances to determine if two double values are close enough
/// to be considered equal.
/// A relative tolerance is useful when comparing values that are large in magnitude, while an absolute tolerance
/// is better suited for comparisons involving values close to zero. Both tolerances are combined to ensure precise
/// and flexible comparisons in a variety of numerical scenarios.
/// The following cases are handled by this comparer:
/// - Floating-point values within computed tolerance thresholds are considered equal.
/// - NaN (Not-a-Number) values are equal to each other, but not to any other value.
/// - Positive and negative infinity values follow IEEE 754 equality rules.
/// - Very small numbers, with absolute values below the specified absolute tolerance, are compared directly.
/// Implements the standard IEqualityComparer{double} interface to support equality comparisons and hash-based collections
/// such as HashSet{double} and Dictionary{double, T}.
public class RelativeDoubleComparer(double relativeTolerance = 1e-3, double absoluteTolerance = 1e-30)
    : IEqualityComparer<double>
{
    /// <inheritdoc />
    public bool Equals(
        double x,
        double y)
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
    public int GetHashCode(
        double obj)
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
