namespace TestFabric.Test;

[TestSubject(typeof(TestClock))]
public class TestClockTests : TestSuite.Normal
{
    [Fact]
    public void Given_Nothing_When_CreateTestClockAndNow_Then_NowIsCurrentTime()
    {
        // Arrange
        var sut = new TestClock();

        // Act
        var actual = sut.Now;
        var timePassed = DateTime.UtcNow - actual;

        // Assert
        Assert.True(timePassed < TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Given_StartTime_When_StartAt_Then_NowIsStartTime()
    {
        // Arrange
        var startTime = RecentDateTimeOffset();
        var sut = new TestClock();

        // Act
        sut.StartAt(startTime);
        var actual = sut.Now;

        // Assert
        Assert.Equal(startTime, actual);
    }

    [Fact]
    public void Given_StartTimeAndTimeSpan_When_StartAtAndAdvance_Then_NowIsStartTimePlusTimePassed()
    {
        // Arrange
        var startTime = RecentDateTimeOffset();
        var timePassed = TimeSpan.FromSeconds(10);
        var sut = new TestClock();

        // Act
        sut.StartAt(startTime);
        sut.Advance(timePassed);
        var actual = sut.Now;

        // Assert
        Assert.Equal(startTime + timePassed, actual);
    }
}
