using RichillCapital.SharedKernel.Monads;
using RichillCapital.Texas.Domain.Entities;
using RichillCapital.Texas.Domain.ValueObjects;

namespace RichillCapital.Texas.Domain.Common;

public interface ITexasService
{
    Task<Result<Session>> NewSessionAsync(int buyInSize = 1000, CancellationToken cancellationToken = default);
    
    Task<Result<Player>> AddPlayerAsync(string name, CancellationToken cancellationToken = default);
    
    Task<Result> BuyInAsync(PlayerId playerId, int groups = 1, CancellationToken cancellationToken = default);
    
    Task<Result> CashOutAsync(PlayerId playerId, int remainingChips, CancellationToken cancellationToken = default);
    
    Task<Result> CloseSessionAsync();

    IEnumerable<Player> GetPlayers();

    int GetPlayerCount();
    
    int GetTotalBuyIn();
}
