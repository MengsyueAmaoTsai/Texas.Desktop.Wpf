using RichillCapital.SharedKernel.Monads;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RichillCapital.Texas.Domain.UnitTests")]

namespace RichillCapital.Texas.Domain;

internal class TexasService : ITexasService
{
    private Maybe<Session> CurrentSession { get; set; }

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
    
    public Result BuyIn(int groups = 1) => Result.Success;
    
    public Result CashOut(int finalChipValue) => Result.Success;
    
    public Result CloseSession() => Result.Success;
    
    public Result<Session> NewSession()
    {
        if (CurrentSession.HasValue)
        {
            return DomainErrors
                .SessionAlreadyExists(CurrentSession.Value.Id)
                .ToResult<Session>();
        }

        var errorOrSession = Session.New();

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
}
