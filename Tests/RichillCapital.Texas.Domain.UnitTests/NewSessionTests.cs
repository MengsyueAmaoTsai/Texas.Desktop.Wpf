using FluentAssertions;
using RichillCapital.Texas.Domain.Services;

namespace RichillCapital.Texas.Domain.UnitTests;

public sealed class NewSessionTests
{
    [Fact]
    public void When_CurrentSessionExists_Should_ReturnsError()
    {
        // Arrange
        var texasService = new TexasService();
        texasService.NewSession();
        
        // Act
        var result = texasService.NewSession();
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("Sessions.AlreadyExists");
    }

    [Fact]
    public void When_CurrentSessionDoesNotExist_Should_ReturnsSession()
    {
        // Arrange
        var texasService = new TexasService();
        
        // Act
        var result = texasService.NewSession();
        
        // Assert
        result.IsSuccess.Should().BeTrue();

        var session = result.Value;
        session.Should().NotBeNull();
        session.Id.Should().NotBeNull();
        session.Players.Should().HaveCount(0);
        session.BuyInSize.Should().Be(TexasService.DefaultBuyInSize);
    }
}