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

        var players = new List<(string Name, int InitialBuyIn)>
        {
            ("Jiayee", 2),
            ("Reno", 3),
            ("Amao", 3),
        };

        var buyIdResults = players
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

        buyIdResults.Should().OnlyContain(result => result.IsSuccess);
        
        var currentSession = texasService.GetCurrentSession().Value;

        var expectedTotalBuyIn = players.Sum(p => p.InitialBuyIn * currentSession.BuyInSize);

        currentSession.Players.Should().HaveCount(players.Count);
        currentSession.TotalBuyIn.Should().Be(expectedTotalBuyIn);
    }
}
