using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public sealed class Session : Entity<SessionId>
{
    private readonly List<Player> _players = [];

    public Session(
        SessionId id) 
        : base(id)
    {
    }

    public IReadOnlyCollection<Player> Players => _players;

    public static ErrorOr<Session> New()
    {
        var newSession = new Session(
            SessionId.NewSessionId());

        return newSession.ToErrorOr();
    }
}

public sealed class Player : Entity<PlayerId>
{
    private Player(PlayerId id) 
        : base(id)
    {
    }
}