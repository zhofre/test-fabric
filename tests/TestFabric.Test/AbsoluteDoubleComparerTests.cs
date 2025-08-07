namespace TestFabric.Test;

public class AbsoluteDoubleComparerTests
{
    [Theory]
    [InlineData(1.0, 1.0, 0.01, true)]
    [InlineData(1.0, 1.1, 0.01, false)]
    [InlineData(1.0, 1.01, 0.01, false)]
    [InlineData(1.0, 1.009, 0.01, true)]
    [InlineData(-1.0, -1.1, 0.01, false)]
    [InlineData(double.NaN, -1.009, 0.01, false)]
    [InlineData(double.NaN, double.NaN, 0.01, true)]
    [InlineData(double.PositiveInfinity, double.PositiveInfinity, 0.01, true)]
    [InlineData(double.NegativeInfinity, double.PositiveInfinity, 0.01, false)]
    public void Given_DoublesAndPrecision_When_Equals_Then_Expected(
        double value1,
        double value2,
        double precision,
        bool expected)
    {
        // Arrange
        var sut = new AbsoluteDoubleComparer(precision);

        // Act
        var actual = sut.Equals(value1, value2);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1.0, 0.01, 1)]
    [InlineData(1.0095, 0.01, 1)]
    [InlineData(1.0, 0.001, 1)]
    [InlineData(double.NaN, 0.001, 0)]
    [InlineData(double.PositiveInfinity, 0.001, int.MaxValue)]
    [InlineData(double.NegativeInfinity, 0.001, int.MinValue)]
    public void Given_DoubleAndPrecision_When_GetHashCode_Then_Expected(
        double value,
        double precision,
        int expected)
    {
        // Arrange
        var sut = new AbsoluteDoubleComparer(precision);

        // Act
        var actual = sut.GetHashCode(value);

        // Assert
        Assert.Equal(expected, actual);
    }
}
