using FluentAssertions;
using RichillCapital.Texas.Domain.Services;

namespace RichillCapital.Texas.Domain.UnitTests;

public sealed class AddPlayerTests
{
    [Fact]
    public void When_SessionNotOpen_Should_ReturnFailureResult()
    {
        // Arrange
        var texasService = new TexasService();

        // Act
        var result = texasService.AddPlayer("Jiayee");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("Sessions.NotOpen");
    }

    [Fact]
    public void When_DuplicatePlayerName_Should_ReturnFailureResult()
    {
        // Arrange
        var texasService = new TexasService();

        texasService.NewSession();

        // Act
        texasService.AddPlayer("jiayee");
        var result = texasService.AddPlayer("jiayee");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("Sessions.DuplicatePlayerName");
    }

    [Fact]
    public void When_MaxPlayersReached_Should_ReturnFailureResult()
    {
        // Arrange
        var texasService = new TexasService();

        texasService.NewSession();

        List<string> players =
        [
            "Jiayee",
            "Reno",
            "小楊",
            "靖彥",
            "QQ",
            "佳瑋",
            "小祐",
            "Amao",
            "阿律",
            "小羽",
            "路人甲",
        ];

        var results = players
            .Select(texasService.AddPlayer);

        // Verify max player count is 10
        results.Count(result => result.IsSuccess).Should().Be(10);
        results.Last().IsFailure.Should().BeTrue();
        results.Last().Error.Code.Should().Be("Sessions.MaxPlayersReached");
    }
}
