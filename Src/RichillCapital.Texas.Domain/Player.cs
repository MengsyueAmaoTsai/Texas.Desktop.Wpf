using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public sealed class Player : Entity<PlayerId>
{
    private Player(
        PlayerId id,
        string name,
        int totalBuyIn) 
        : base(id)
    {
        Name = name;
        TotalBuyIn = totalBuyIn;
    }

    public string Name { get; private set; }

    public int TotalBuyIn { get; private set; }

    public static ErrorOr<Player> Create(
        PlayerId id, 
        string name)
    {
        var player = new Player(
            id, 
            name,
            totalBuyIn: 0);

        return player
            .ToErrorOr();
    }

    public Result BuyIn(int size)
    {
        TotalBuyIn += size;

        return Result.Success;
    }
}