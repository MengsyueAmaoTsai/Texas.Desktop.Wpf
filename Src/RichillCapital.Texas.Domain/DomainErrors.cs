using RichillCapital.SharedKernel;

namespace RichillCapital.Texas.Domain;

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
}
