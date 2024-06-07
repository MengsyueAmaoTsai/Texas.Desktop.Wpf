using RichillCapital.SharedKernel;
using RichillCapital.Texas.Domain;

namespace RichillCapital.Texas.Desktop;

public static class DomainErrors
{
    public static Error SessionAlreadyExists(SessionId sessionId) => Error
        .Conflict("Sessions.AlreadyExists", $"Session {sessionId} already exists");
}
