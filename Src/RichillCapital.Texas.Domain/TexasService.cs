using RichillCapital.SharedKernel.Monads;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RichillCapital.Texas.Domain.UnitTests")]

namespace RichillCapital.Texas.Domain;

internal class TexasService : ITexasService
{
    private Maybe<Session> CurrentSession { get; set; }

    public Result AddPlayer(string id, string name)
    {
        return Result.Success;
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
