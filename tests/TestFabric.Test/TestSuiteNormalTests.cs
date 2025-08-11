using System.Globalization;

// ReSharper disable ReplaceAutoPropertyWithComputedProperty

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace TestFabric.Test;

[TestSubject(typeof(TestSuite.Normal))]
public class TestSuiteNormalTests(ITestOutputHelper logger) : TestSuite.Normal
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
    public void Given_NormalTestClassAndCount_When_RandomInt_Then_IntegersCreated()
    {
        // Arrange
        const int count = 3;

        // Act
        var actual = Random<int>(count);

        // Assert
        Assert.Equal(count, actual.Count());
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
    public void Given_NormalTestClass_When_InRangeLong_Then_LongIntegerCreated()
    {
        // Arrange
        const long minInclusive = 3;
        const long maxExclusive = 10;

        // Act
        var actual = InRange(minInclusive, maxExclusive);

        // Assert
        Assert.True(actual >= minInclusive);
        Assert.True(actual < maxExclusive);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeLongItems_Then_LongIntegerCreated()
    {
        // Arrange
        long[] items = [3, 5, 7];

        // Act
        var actual = InRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeFloat_Then_FloatCreated()
    {
        // Arrange
        const float minInclusive = 3.1f;
        const float maxExclusive = 9.9f;

        // Act
        var actual = InRange(minInclusive, maxExclusive);

        // Assert
        Assert.True(actual >= minInclusive);
        Assert.True(actual < maxExclusive);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeFloatItems_Then_FloatCreated()
    {
        // Arrange
        float[] items = [3.1f, 4.9f, 7.3f];

        // Act
        var actual = InRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDecimal_Then_DecimalCreated()
    {
        // Arrange
        const decimal minInclusive = 3.1m;
        const decimal maxExclusive = 9.9m;

        // Act
        var actual = InRange(minInclusive, maxExclusive);

        // Assert
        Assert.True(actual >= minInclusive);
        Assert.True(actual < maxExclusive);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDecimalItems_Then_DecimalCreated()
    {
        // Arrange
        decimal[] items = [3.1m, 4.9m, 7.3m];

        // Act
        var actual = InRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDouble_Then_DoubleCreated()
    {
        // Arrange
        const double minInclusive = 3.1;
        const double maxExclusive = 9.9;

        // Act
        var actual = InRange(minInclusive, maxExclusive);

        // Assert
        Assert.True(actual >= minInclusive);
        Assert.True(actual < maxExclusive);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDoubleItems_Then_DoubleCreated()
    {
        // Arrange
        double[] items = [3.1, 4.9, 7.3];

        // Act
        var actual = InRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDateTime_Then_DateTimeCreated()
    {
        // Arrange
        var minInclusive = new DateTime(2023, 1, 1);
        var maxExclusive = new DateTime(2024, 1, 1);

        // Act
        var actual = InRange(minInclusive, maxExclusive);

        // Assert
        Assert.True(actual >= minInclusive);
        Assert.True(actual < maxExclusive);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDateTimeItems_Then_DateTimeCreated()
    {
        // Arrange
        DateTime[] items = [new(2023, 1, 1), new(2023, 6, 1), new(2023, 12, 31)];

        // Act
        var actual = InRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDateTimeOffset_Then_DateTimeOffsetCreated()
    {
        // Arrange
        var minInclusive = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var maxExclusive = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

        // Act
        var actual = InRange(minInclusive, maxExclusive);

        // Assert
        Assert.True(actual >= minInclusive);
        Assert.True(actual < maxExclusive);
    }

    [Fact]
    public void Given_NormalTestClass_When_InRangeDateTimeOffsetItems_Then_DateTimeOffsetCreated()
    {
        // Arrange
        DateTimeOffset[] items =
        [
            new(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new(2023, 6, 1, 0, 0, 0, TimeSpan.Zero),
            new(2023, 12, 31, 0, 0, 0, TimeSpan.Zero)
        ];

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
    public void Given_NormalTestClassAndCount_When_InRangeInt_Then_IntegersCreated()
    {
        // Arrange
        const int count = 3;
        const int minInclusive = 3;
        const int maxExclusive = 10;

        // Act
        var actual = InRange(count, minInclusive, maxExclusive)
            .ToArray();

        // Assert
        Assert.Equal(count, actual.Length);
        Assert.True(actual.All(x => x >= minInclusive));
        Assert.True(actual.All(x => x < maxExclusive));
    }

    [Fact]
    public void Given_NormalTestClassAndCount_When_InRangeIntItems_Then_IntegersCreated()
    {
        // Arrange
        const int count = 3;
        int[] items = [3, 5, 7];

        // Act
        var actual = InRange(count, items)
            .ToArray();

        // Assert
        Assert.Equal(count, actual.Length);
        Assert.True(actual.All(x => items.Contains(x)));
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

    [Fact]
    public void Given_Nothing_When_FirstNameCalled_Then_RandomFirstName()
    {
        // Arrange

        // Act
        var actual = FirstName();

        // Assert
        Assert.Contains(actual, FirstNames);
    }

    [Fact]
    public void Given_Nothing_When_LastNameCalled_Then_RandomLastName()
    {
        // Arrange

        // Act
        var actual = LastName();

        // Assert
        Assert.Contains(actual, LastNames);
    }

    [Fact]
    public void Given_Nothing_When_FullNameCalledWithSecondLastName_Then_CorrectParts()
    {
        // Arrange

        // Act
        var fullName = FullName(true);
        var parts = fullName.Split(' ');

        // Assert
        Assert.Equal(3, parts.Length);
        Assert.Contains(parts[0], FirstNames);
    }

    [Fact]
    public void Given_Nothing_When_FullNameCalledWithoutSecondLastName_Then_CorrectParts()
    {
        // Arrange

        // Act
        var fullName = FullName();
        var parts = fullName.Split(' ');

        // Assert
        Assert.Equal(2, parts.Length);
        Assert.Contains(parts[0], FirstNames);
    }

    [Fact]
    public void Given_Nothing_When_GenerateEmail_Then_EmailIsValid()
    {
        // Arrange

        // Act
        var email = Email();
        var parts = email.Split('@', '.');

        // Act & Assert
        Assert.Contains(parts[0], FirstNames.Select(name => name.ToLower()));
        Assert.Contains(parts[1], LastNames.Select(name => name.ToLower()));
        Assert.Contains($"@{parts[2]}.{parts[3]}", EmailDomains);
    }

    [Fact]
    public void Given_Nothing_When_CompanyNameCalled_Then_RandomCompany()
    {
        // Arrange

        // Act
        var actual = CompanyName();

        // Assert
        Assert.Contains(actual, CompanyNames);
    }

    [Fact]
    public void Given_Nothing_When_CountryCalled_Then_RandomCountry()
    {
        // Arrange

        // Act
        var actual = Country();

        // Assert
        Assert.Contains(actual, Countries);
    }

    [Fact]
    public void Given_CountryResult_When_CallCityMethod_Then_ValidResponse()
    {
        // Arrange
        var country = Country();

        // Act
        var city = City(country);

        // Assert
        Assert.NotEmpty(city);
        Assert.Contains(city, Cities[country]);
    }

    [Fact]
    public void Given_RandomStringInput_When_CallCityMethod_Then_ThrowsException()
    {
        // Arrange
        var randomString = Guid.NewGuid().ToString();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => City(randomString));
    }

    [Fact]
    public void Given_DaysBack_When_RecentDateTime_Then_DateWithinDaysBack()
    {
        // Arrange
        const int daysBack = 10;

        // Act
        var actual = RecentDateTime(daysBack);

        // Assert
        var now = DateTime.UtcNow;
        logger.WriteLine($"Now: {now}");
        logger.WriteLine($"Actual: {actual}");
        Assert.True(actual >= now.AddDays(-daysBack));
        Assert.True(actual <= now);
    }

    [Fact]
    public void Given_DaysBack_When_RecentDateTimeOffset_Then_DateWithinDaysBack()
    {
        // Arrange
        const int daysBack = 10;

        // Act
        var actual = RecentDateTimeOffset(daysBack);

        // Assert
        var now = DateTimeOffset.UtcNow;
        logger.WriteLine($"Now: {now}");
        logger.WriteLine($"Actual: {actual}");
        Assert.True(actual >= now.AddDays(-daysBack));
        Assert.True(actual <= now);
    }

    [Fact]
    public void Given_DummyClass_When_FromTemplateAge_Then_SameButAgeDifferent()
    {
        // Arrange
        var dummy = Random<DummyClass>();

        // Act
        var actual = FromTemplate(dummy, x => x.Age);

        // Assert
        Assert.Equal(dummy.Name, actual.Name);
        Assert.NotEqual(dummy.Age, actual.Age);
        Assert.Equal(dummy.Title, actual.Title);
    }

    [Fact]
    public void Given_DummyClassAndCount_When_FromTemplateName_Then_MultipleSameButNameDifferent()
    {
        // Arrange
        var dummy = Random<DummyClass>();
        var count = InRange(3, 7);

        // Act
        var actual = FromTemplate(
                count,
                dummy,
                x => x.Name)
            .ToArray();

        // Assert
        Assert.Equal(count, actual.Length);
        foreach (var item in actual)
        {
            Assert.NotEqual(dummy.Name, item.Name);
            Assert.Equal(dummy.Age, item.Age);
            Assert.Equal(dummy.Title, item.Title);
        }
    }

    public class DummyClass
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string Title { get; } = "Dr.";
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private class RecursionDummy
    {
        // ReSharper disable once UnusedMember.Local
        public RecursionDummy? RecursiveItem { get; set; }
    }
}
