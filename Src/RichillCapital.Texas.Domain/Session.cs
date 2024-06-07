using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public sealed class Session : Entity<SessionId>
{
    public Session(SessionId id) : base(id)
    {
    }

    public static ErrorOr<Session> New()
    {
        var newSession = new Session(
            SessionId.NewSessionId());

        return newSession.ToErrorOr();
    }
}
