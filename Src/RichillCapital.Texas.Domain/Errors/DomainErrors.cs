using RichillCapital.SharedKernel;
using RichillCapital.Texas.Domain.ValueObjects;

namespace RichillCapital.Texas.Domain.Errors;

public static class DomainErrors
{
    public static readonly Error SessionNotOpen = Error
        .Invalid("Sessions.NotOpen", "Session is not open");

    public static Error SessionAlreadyExists(SessionId sessionId) => Error
        .Invalid("Sessions.AlreadyExists", $"Session {sessionId} already exists");

    public static Error DuplicatePlayerName(string playerName) => Error
        .Conflict("Sessions.DuplicatePlayerName", $"Player {playerName} already exists");

    public static Error MaxPlayersReached(int maxPlayers) => Error
        .Conflict("Sessions.MaxPlayersReached", $"Max players reached: {maxPlayers}");

    public static Error PlayerNotFound(PlayerId playerId) => Error
        .NotFound("Players.NotFound", $"Player {playerId} not found");

    public static Error PlayerWithoutBuyIn = Error
        .Conflict("Players.WithoutBuyIn", "Player does not have a buy-in");

    public static Error PlayerAlreadyCashedOut(PlayerId id) =>
        Error.Conflict("Players.AlreadyCashedOut", $"Player {id} already cashed out");
}
