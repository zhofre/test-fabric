namespace TestFabric.Test;

public class RelativeDoubleComparerTests
{
    [Theory]
    [InlineData(1, 1.02, 1e-2, 1e-6, false)]
    [InlineData(1, 1.005, 1e-2, 1e-6, true)]
    [InlineData(1, 0.98, 1e-2, 1e-6, false)]
    [InlineData(1, 0.995, 1e-2, 1e-6, true)]
    [InlineData(0, 0.1, 1e-2, 1e-2, false)]
    [InlineData(0, 0.001, 1e-2, 1e-2, true)]
    [InlineData(0.1, 0, 1e-2, 1e-2, false)]
    [InlineData(0.001, 0, 1e-2, 1e-2, true)]
    [InlineData(-0.00010755, -0.00010755000000000005, 1e-3, 1e-6, true)]
    [InlineData(-0.00010755, 0.00010755000000000005, 1e-3, 1e-6, false)]
    [InlineData(double.NaN, -1.009, 0.01, 1e-6, false)]
    [InlineData(double.NaN, double.NaN, 0.01, 1e-6, true)]
    [InlineData(double.PositiveInfinity, double.PositiveInfinity, 0.01, 1e-6, true)]
    [InlineData(double.NegativeInfinity, double.PositiveInfinity, 0.01, 1e-6, false)]
    public void Given_TwoDoublesAndPrecision_When_Equals_Then_Expected(
        double x,
        double y,
        double relativePrecision,
        double absolutePrecision,
        bool expected)
    {
        // Arrange
        var sut = new RelativeDoubleComparer(relativePrecision, absolutePrecision);

        // Act
        var actual = sut.Equals(x, y);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(1.0, 0.01, 1e-6, 1)]
    [InlineData(1.0095, 0.01, 1e-6, 1)]
    [InlineData(1.0, 0.001, 1e-6, 1)]
    [InlineData(double.NaN, 0.001, 1e-6, 0)]
    [InlineData(double.PositiveInfinity, 0.001, 1e-6, int.MaxValue)]
    [InlineData(double.NegativeInfinity, 0.001, 1e-6, int.MinValue)]
    public void Given_DoubleAndPrecision_When_GetHashCode_Then_Expected(
        double value,
        double relativePrecision,
        double absolutePrecision,
        int expected)
    {
        // Arrange
        var sut = new RelativeDoubleComparer(relativePrecision, absolutePrecision);

        // Act
        var actual = sut.GetHashCode(value);

        // Assert
        actual.Should().Be(expected);
    }
}
