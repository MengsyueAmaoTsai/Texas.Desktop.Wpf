using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public sealed class Session : Entity<SessionId>
{
    private const int MaxPlayers = 10;

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

    public Result AddPlayer(Player player)
    {
        if (_players.Count >= MaxPlayers)
        {
            return DomainErrors
                .MaxPlayersReached(MaxPlayers)
                .ToResult();
        }

        if (_players.Any(p => p.Name == player.Name))
        {
            return DomainErrors
                .DuplicatePlayerName(player.Name)
                .ToResult();
        }

        _players.Add(player);

        return Result.Success;
    }
}
