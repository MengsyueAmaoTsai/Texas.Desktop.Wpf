using FluentAssertions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.Texas.Domain.Errors;
using RichillCapital.Texas.Domain.Services;

namespace RichillCapital.Texas.Domain.UnitTests;

public sealed class CashOutTests
{

    [Fact]
    public void When_PlayerWithoutBuyIn_Should_ReturnFailure()
    {
        // Assert
        var service = new TexasService();

        service.NewSession();
        var playerResult = service.AddPlayer("JiaYee").ThrowIfFailure();

        // Act
        var cashOutResult = service.CashOut(playerResult.Value.Id, 3000);

        // Assert
        cashOutResult.IsFailure.Should().BeTrue();
        cashOutResult.Error.Code.Should().Be(DomainErrors.PlayerWithoutBuyIn.Code);
    }

    [Fact]
    public void When_PlayerAlreadyCashedOut_Should_ReturnFailure()
    {

        // Assert
        var service = new TexasService();

        service.NewSession();
        var playerResult = service
            .AddPlayer("JiaYee")
            .ThrowIfFailure();

        _ = service.BuyIn(playerResult.Value.Id, 3000);
        _ = service.CashOut(playerResult.Value.Id, 1500);

        // Act
        var cashOutResult = service.CashOut(playerResult.Value.Id, 1500);

        // Assert
        cashOutResult.IsFailure.Should().BeTrue();
        cashOutResult.Error.Code.Should().Be("Players.AlreadyCashedOut");
    }

    [Fact]
    public void Should_ReturnSuccess()
    {
        // Arrange
        var service = new TexasService();

        service.NewSession();

        var players = new List<(string Name, int InitialBuyIn, int RemainingChips)>
        {
            ("Jiayee", 2, 3000),
            ("Reno", 3, 1500),
            ("Amao", 3, 3500),
        }
        .Select(p => (service.AddPlayer(p.Name).Value.Id, p.InitialBuyIn, p.RemainingChips))
        .ToList();

        foreach (var player in players)
        {
            _ = service.BuyIn(player.Id, player.InitialBuyIn);
        }

        var currentSession = service.GetCurrentSession().Value;

        var firstPlayer = players.First();

        service.CashOut(firstPlayer.Id, firstPlayer.RemainingChips);

        var expectedTotalBuyIn = players.Sum(p => p.InitialBuyIn * currentSession.BuyInSize);

        // Act
        currentSession.TotalBuyIn.Should().Be(expectedTotalBuyIn);
        currentSession.TotalCashOut.Should().Be(firstPlayer.RemainingChips);
        currentSession.TotalChips.Should().Be(expectedTotalBuyIn - firstPlayer.RemainingChips);

        var restOfPlayers = players.Where(p => p.Id != firstPlayer.Id).ToList();

        foreach (var player in restOfPlayers)
        {
            var cashOutResult = service.CashOut(player.Id, player.RemainingChips);

            cashOutResult.IsSuccess.Should().BeTrue();
        }

        // Assert
        currentSession.TotalBuyIn.Should().Be(expectedTotalBuyIn);
        currentSession.TotalCashOut.Should().Be(players.Sum(p => p.RemainingChips));
        currentSession.TotalChips.Should().Be(0);
    }
}
