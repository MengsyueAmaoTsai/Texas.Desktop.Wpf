using RichillCapital.SharedKernel;

namespace RichillCapital.Texas.Domain;

public static class DomainErrors
{
    public static Error SessionAlreadyExists(SessionId sessionId) => Error
        .Conflict("Sessions.AlreadyExists", $"Session {sessionId} already exists");
}
