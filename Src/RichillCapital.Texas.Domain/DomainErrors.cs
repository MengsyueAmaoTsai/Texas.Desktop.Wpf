using RichillCapital.SharedKernel;

namespace RichillCapital.Texas.Domain;

public static class DomainErrors
{
    public static readonly Error SessionNotOpen = Error
        .Conflict("Sessions.NotOpen", "Session is not open");
    
    public static Error SessionAlreadyExists(SessionId sessionId) => Error
        .Conflict("Sessions.AlreadyExists", $"Session {sessionId} already exists");

    public static Error MaxPlayersReached(int maxPlayers) => Error
        .Conflict("Sessions.MaxPlayersReached", $"Max players reached: {maxPlayers}");
    
    public static Error DuplicatePlayerName(string playerName) => Error
        .Conflict("Sessions.DuplicatePlayerName", $"Player {playerName} already exists");
}
