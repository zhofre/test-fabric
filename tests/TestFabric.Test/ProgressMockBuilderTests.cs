namespace TestFabric.Test;

public class ProgressMockBuilderTests
{
    [Fact]
    public void Given_Progress_When_Report_Then_Success()
    {
        // Arrange
        var sut = new ProgressMockBuilder<int>()
            .WithReport()
            .Create();

        // Act
        sut.Object.Report(1);

        // Assert
        // no error
    }

    [Fact]
    public void Given_ProgressCallback_When_Report_Then_Called()
    {
        // Arrange
        var callbackExecuted = false;
        var sut = new ProgressMockBuilder<int>()
            .WithReport(_ => callbackExecuted = true)
            .Create();

        // Act
        sut.Object.Report(1);

        // Assert
        callbackExecuted.Should().BeTrue();
    }
}
