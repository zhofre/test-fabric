using System.Globalization;
using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test;

[TestSubject(typeof(TestSuite.Normal))]
public class TestSuiteNormalTests : TestSuite.Normal
{
    [Fact]
    public void Given_Nothing_When_CreateGuid_Then_FactoryUsed()
    {
        // Arrange
        // Act
        var actual = Factory.Create<Guid>();

        // Assert
        Assert.NotEqual(Guid.Empty, actual);
    }

    [Fact]
    public void Given_Nothing_When_CreateInt_Then_SmallerThanNormalBound()
    {
        // Arrange
        // Act
        var actual = Factory.Create<int>();

        // Assert
        Assert.True(-RandomIntFactory.MediumBound < actual);
        Assert.True(actual < RandomIntFactory.MediumBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateDouble_Then_SmallerThanNormalBound()
    {
        // Arrange
        // Act
        var actual = Factory.Create<double>();

        // Assert
        Assert.True(-RandomDoubleFactory.MediumBound < actual);
        Assert.True(actual < RandomDoubleFactory.MediumBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateRecursionDummy_Then_Exception()
    {
        // Arrange
        // Act+Assert
        Assert.Throws<FactoryException>(Factory.Create<RecursionDummy>);
    }

    [Theory]
    [InlineData(1.2, "en-US", "1.2")]
    [InlineData(1.2, "nl-BE", "1,2")]
    public void Given_DoubleAndNormalTestClass_When_SetCultureAndToString_Then_Expected(
        double input,
        string culture,
        string expected)
    {
        // Arrange
        // Act
        SetCurrentCulture(culture);
        var actual = input.ToString(CultureInfo.CurrentCulture);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_NormalTestClass_When_SetCultureInvariant_Then_CurrentCultureInvariant()
    {
        // Arrange
        var expected = CultureInfo.InvariantCulture;

        // Act
        SetCurrentCultureInvariant();
        var actual = Thread.CurrentThread.CurrentCulture;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_NormalTestClass_When_CreateRandomDoubleFactoryTwice_Then_Same_Instance()
    {
        // Arrange
        // Act
        var actual1 = Factory.Create<RandomDoubleFactory>();
        var actual2 = Factory.Create<RandomDoubleFactory>();

        // Assert
        Assert.Same(actual1, actual2);
    }

    [Fact]
    public void Given_NormalTestClass_When_RandomInt_Then_IntegerCreated()
    {
        // Arrange
        // Act
        var actual = Random<int>();

        // Assert
        Assert.True(actual >= -RandomIntFactory.LargeBound);
        Assert.True(actual <= RandomIntFactory.LargeBound);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeInt_Then_IntegerCreated()
    {
        // Arrange
        const int minInclusive = 3;
        const int maxExclusive = 10;

        // Act
        var actual = InRange(minInclusive, maxExclusive);

        // Assert
        Assert.True(actual >= minInclusive);
        Assert.True(actual < maxExclusive);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeIntItems_Then_IntegerCreated()
    {
        // Arrange
        int[] items = [3, 5, 7];

        // Act
        var actual = InRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeStringItems_Then_StringCreated()
    {
        // Arrange
        string[] items = ["hello", "world", "test"];

        // Act
        var actual = InRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_RecursionDummies_When_UpdateRecursiveItem_Then_Updated()
    {
        // Arrange
        var dummy1 = new RecursionDummy();
        var dummy2 = new RecursionDummy();

        // Act
        dummy1.RecursiveItem = dummy2;
        var actual = dummy1.RecursiveItem;

        // Assert
        Assert.Same(dummy2, actual);
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private class RecursionDummy
    {
        // ReSharper disable once UnusedMember.Local
        public RecursionDummy? RecursiveItem { get; set; }
    }
}
