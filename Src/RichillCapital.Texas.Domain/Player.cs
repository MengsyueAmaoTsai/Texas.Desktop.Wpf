using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using System.Numerics;

namespace RichillCapital.Texas.Domain;

public sealed class Player : Entity<PlayerId>
{
    private Player(
        PlayerId id,
        string name,
        int totalBuyIn,
        int totalCashOut) 
        : base(id)
    {
        Name = name;
        TotalBuyIn = totalBuyIn;
        TotalCashOut = totalCashOut;    
    }

    public string Name { get; private set; }

    public int TotalBuyIn { get; private set; }
    public int TotalCashOut { get; private set; }

    public static ErrorOr<Player> Create(
        PlayerId id, 
        string name)
    {
        var player = new Player(
            id, 
            name,
            totalBuyIn: 0,
            totalCashOut: 0);

        return player
            .ToErrorOr();
    }

    public Result BuyIn(int buyInSize)
    {
        TotalBuyIn += buyInSize;

        return Result.Success;
    }

    public Result CashOut(int remainingSize)
    {
        if (TotalBuyIn == 0)
        {
            return DomainErrors
                .PlayerWithoutBuyIn
                .ToResult();
        }

        if (TotalBuyIn != 0 && TotalCashOut != 0)
        {
            return DomainErrors
                .PlayerAlreadyCashedOut(Id)
                .ToResult();
        }

        TotalCashOut = remainingSize;

        return Result.Success;
    }
}