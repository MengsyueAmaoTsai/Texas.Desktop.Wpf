using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public sealed class Session : Entity<SessionId>
{
    private const int MaxPlayers = 10;

    private readonly List<Player> _players = [];

    public Session(
        SessionId id,
        int buyInSize) 
        : base(id)
    {
        BuyInSize = buyInSize;
    }
    
    public int BuyInSize { get; private set; }

    public IReadOnlyCollection<Player> Players => _players;

    public int TotalBuyIn => _players.Sum(p => p.TotalBuyIn);
    public int TotalCashOut => _players.Sum(p => p.TotalCashOut);
    public int TotalChips => TotalBuyIn - TotalCashOut;

    public static ErrorOr<Session> New(int buyInSize)
    {
        var newSession = new Session(
            SessionId.NewSessionId(),
            buyInSize);

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
