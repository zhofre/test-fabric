namespace TestFabric.Test;

public class LoggerMockBuilderTests
{
    [Fact]
    public void Given_Logger_When_LogTrace_Then_Success()
    {
        // Arrange
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog()
            .Create();

        // Act
        sut.Object.LogTrace("trace");

        // Assert
        // no error
    }

    [Fact]
    public void Given_Logger_When_LogDebug_Then_Success()
    {
        // Arrange
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog()
            .Create();

        // Act
        sut.Object.LogDebug("debug");

        // Assert
        // no error
    }

    [Fact]
    public void Given_Logger_When_LogInformation_Then_Success()
    {
        // Arrange
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog()
            .Create();

        // Act
        sut.Object.LogInformation("info");

        // Assert
        // no error
    }

    [Fact]
    public void Given_Logger_When_LogWarning_Then_Success()
    {
        // Arrange
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog()
            .Create();

        // Act
        sut.Object.LogWarning("warn");

        // Assert
        // no error
    }

    [Fact]
    public void Given_Logger_When_LogError_Then_Success()
    {
        // Arrange
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog()
            .Create();

        // Act
        sut.Object.LogError("error");

        // Assert
        // no error
    }

    [Fact]
    public void Given_Logger_When_LogCritical_Then_Success()
    {
        // Arrange
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog()
            .Create();

        // Act
        sut.Object.LogCritical("critical");

        // Assert
        // no error
    }

    [Fact]
    public void Given_LoggerWithLog_When_LogInformation_Then_TextAdded()
    {
        // Arrange
        var log = new List<string>();
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog(log)
            .Create();
        const string expected = "[Information] Hello World!";

        // Act
        sut.Object.LogInformation(
            "Hello {name}!",
            "World");

        // Assert
        log.Should().ContainSingle();
        log.First().Should().Be(expected);
    }

    [Fact]
    public void Given_LoggerWithLog_When_LogErrorWithException_Then_TextAdded()
    {
        // Arrange
        var log = new List<string>();
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog(log)
            .Create();
        const string expected = "[Error] Hello World! | My exception message";

        // Act
        sut.Object.LogError(
            new Exception("My exception message"),
            "Hello {name}!",
            "World");

        // Assert
        log.Should().ContainSingle();
        log.First().Should().Be(expected);
    }

    [Fact]
    public void Given_LoggerWithHandler_When_LogErrorWithException_Then_TextAdded()
    {
        // Arrange
        var response = "";
        var sut = new LoggerMockBuilder<Foo>()
            .WithLog((l, s, e) =>
            {
                response = $"|{l}|{s}|{e?.Message}";
            })
            .Create();
        const string expected = "|Error|Hello World!|My exception message";

        // Act
        sut.Object.LogError(
            new Exception("My exception message"),
            "Hello {name}!",
            "World");

        // Assert
        Assert.Equal(expected, response);
    }

    public class Foo;
}
