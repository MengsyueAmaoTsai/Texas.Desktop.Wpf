using FluentAssertions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.Texas.Domain.Services;

namespace RichillCapital.Texas.Domain.UnitTests;

public sealed class BuyInTests
{
    [Fact]
    public void When_SessionIsOpenAndPlayerAdded_Should_ReturnSuccess()
    {
        // Arrange
        var texasService = new TexasService();
        var players = new List<(string Name, int InitialBuyIn)>
        {
            ("Jiayee", 2),
            ("Reno", 3),
            ("Amao", 3),
        };

        // Act
        texasService.NewSession();

        var buyInResults = players
            .Select(p =>
            {
                var addPlayerResult = texasService.AddPlayer(p.Name);

                if (addPlayerResult.IsFailure)
                {
                    return addPlayerResult.Error.ToResult();
                }

                var player = addPlayerResult.Value;

                var buyIdResult = texasService.BuyIn(player.Id, p.InitialBuyIn);

                return buyIdResult;
            });

        var currentSession = texasService.GetCurrentSession().Value;
        var expectedTotalBuyIn = players.Sum(p => p.InitialBuyIn * currentSession.BuyInSize);

        // Assert
        buyInResults.Should().OnlyContain(result => result.IsSuccess);
        currentSession.Players.Should().HaveCount(players.Count);
        currentSession.TotalBuyIn.Should().Be(expectedTotalBuyIn);
        currentSession.TotalCashOut.Should().Be(0);
    }
}
