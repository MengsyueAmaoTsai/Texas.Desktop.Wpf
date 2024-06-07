using FluentAssertions;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain.UnitTests;

public sealed class BuyInTests
{
    [Fact]
    public async Task When()
    {
        var texasService = new TexasService();

        texasService.NewSession();

        var players = new List<string>
        {
            "Jiayee",
            "Reno",
            "Amao",
        };

        var buyIdResults = players
            .Select(playerName =>
            {
                var addPlayerResult = texasService.AddPlayer(playerName);

                if (addPlayerResult.IsFailure)
                {
                    return addPlayerResult.Error.ToResult();
                }

                var player = addPlayerResult.Value;

                var buyIdResult = texasService.BuyIn(player.Id, 1);

                return buyIdResult;
            });

        buyIdResults.Should().OnlyContain(result => result.IsSuccess);
        
        var currentSession = texasService.GetCurrentSession().Value;

        currentSession.Players.Should().HaveCount(players.Count);
        currentSession.TotalBuyIn.Should().Be(players.Count * 1000);
    }
}
