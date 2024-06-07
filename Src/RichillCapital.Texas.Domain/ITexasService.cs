using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public interface ITexasService
{
    Result<Session> NewSession(int buyInSize = 1000);
    Result<Player> AddPlayer(string name);
    Result BuyIn(PlayerId playerId, int groups = 1);
    Result CashOut(int finalChipValue);
    Result CloseSession();
    int GetPlayerCount();
    int GetTotalBuyIn();
}
