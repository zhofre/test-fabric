using JetBrains.Annotations;
using Sample.Library.TestTools;
using TestFabric;

namespace Sample.Library.xUnit.Test;

[TestSubject(typeof(OrderFactory))]
public class OrderFactoryTests : TestSuite.Normal
{
    [Fact]
    public void Given_Customer_When_Create_Then_Success()
    {
        // Arrange
        var id = Random<Guid>();
        var guidGeneratorMock = new GuidGeneratorMockBuilder()
            .WithGenerate(id)
            .Create();
        var timeStampGeneratorMock = new TimeStampGeneratorMockBuilder()
            .WithGenerate(RecentDateTimeOffset())
            .Create();
        var sut = new OrderFactory(
            guidGeneratorMock.Object,
            timeStampGeneratorMock.Object);
        var customer = Random<Person>();

        // Act
        var actual = sut.Create(customer);

        // Assert
        actual.Should().NotBeNull();
        actual.Id.Should().Be(id);
        actual.Customer.Should().Be(customer);
    }

    [Fact]
    public void Given_TestClock_When_AdvanceFiveMinutes_Then_TimeIsFiveMinutesLater()
    {
        // Arrange
        var id = Random<Guid>();
        var testClock = new TestClock();
        var guidGeneratorMock = new GuidGeneratorMockBuilder()
            .WithGenerate(id)
            .Create();
        var timeStampGeneratorMock = new TimeStampGeneratorMockBuilder()
            .WithGenerate(testClock)
            .Create();
        var sut = new OrderFactory(
            guidGeneratorMock.Object,
            timeStampGeneratorMock.Object);
        var customer = Random<Person>();
        var startTime = RecentDateTimeOffset();
        const int minutes = 5;

        // Act
        testClock.StartAt(startTime);
        var order1 = sut.Create(customer);
        testClock.Advance(TimeSpan.FromMinutes(minutes));
        var order2 = sut.Create(customer);
        var actual = order2.OrderDate - order1.OrderDate;

        // Assert
        actual.Should().Be(TimeSpan.FromMinutes(minutes));
    }
}
