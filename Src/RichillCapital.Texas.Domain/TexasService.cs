using RichillCapital.SharedKernel.Monads;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RichillCapital.Texas.Domain.UnitTests")]

namespace RichillCapital.Texas.Domain;

internal class TexasService : ITexasService
{
    internal const int DefaultBuyInSize = 1000;

    private Maybe<Session> CurrentSession { get; set; }

    public Maybe<Session> GetCurrentSession() => CurrentSession;

    public Result<Player> AddPlayer(string name)
    {
        if (CurrentSession.IsNull)
        {
            return DomainErrors
                .SessionNotOpen
                .ToResult<Player>();
        }

        var idResult = PlayerId.From(CurrentSession.Value.Players.Count + 1);

        if (idResult.IsFailure)
        {
            return idResult.Error.ToResult<Player>();
        }

        var errorOrPlayer = Player.Create(idResult.Value, name);
        
        if (errorOrPlayer.HasError)
        {
            return errorOrPlayer.Errors.First()
                .ToResult<Player>();
        }

        var addPlayerResult = CurrentSession.Value.AddPlayer(errorOrPlayer.Value);

        if (addPlayerResult.IsFailure)
        {
            return addPlayerResult.Error.ToResult<Player>();
        }

        return errorOrPlayer.Value.ToResult();
    }
    
    public Result BuyIn(PlayerId playerId, int groups = 1)
    {
        var maybePlayer = GetPlayer(playerId);

        if (maybePlayer.IsNull)
        {
            return DomainErrors
                .PlayerNotFound(playerId)
                .ToResult();
        }

        var player = maybePlayer.Value;

        player.BuyIn(groups * CurrentSession.Value.BuyInSize);

        return Result.Success;
    }
    
    public Result CashOut(int finalChipValue) => Result.Success;
    
    public Result CloseSession() => Result.Success;
    
    public Result<Session> NewSession(int buyInSize = DefaultBuyInSize)
    {
        if (CurrentSession.HasValue)
        {
            return DomainErrors
                .SessionAlreadyExists(CurrentSession.Value.Id)
                .ToResult<Session>();
        }

        var errorOrSession = Session.New(buyInSize);

        if (errorOrSession.HasError)
        {
            return errorOrSession
                .Errors.First()
                .ToResult<Session>();
        }

        var newSession = errorOrSession.Value;

        CurrentSession = newSession.ToMaybe();

        return newSession.ToResult();
    }

    public int GetPlayerCount() => CurrentSession.HasValue ? 
        CurrentSession.Value.Players.Count : 0;

    public int GetTotalBuyIn() => CurrentSession.HasValue ? 
        CurrentSession.Value.Players.Sum(player => player.TotalBuyIn) : 0;

    private Maybe<Player> GetPlayer(PlayerId id)
    {
        var player = CurrentSession.Value.Players
            .FirstOrDefault(p => p.Id == id);

        return Maybe<Player>.With(player!);
    }
}
