using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public sealed class Player : Entity<PlayerId>
{
    private Player(
        PlayerId id,
        string name) 
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static ErrorOr<Player> Create(PlayerId id, string name)
    {
        var player = new Player(id, name);

        return player
            .ToErrorOr();
    }
}