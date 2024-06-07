using RichillCapital.SharedKernel.Monads;
using RichillCapital.Texas.Domain.Entities;
using RichillCapital.Texas.Domain.ValueObjects;

namespace RichillCapital.Texas.Domain.Common;

public interface ITexasService
{
    Result<Session> NewSession(int buyInSize = 1000);
    Result<Player> AddPlayer(string name);
    Result BuyIn(PlayerId playerId, int groups = 1);
    Result CashOut(PlayerId playerId, int remainingChips);
    Result CloseSession();
    int GetPlayerCount();
    int GetTotalBuyIn();
}
