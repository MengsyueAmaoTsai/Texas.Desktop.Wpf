using RichillCapital.SharedKernel;

namespace RichillCapital.Texas.Domain;

public sealed class SessionId : SingleValueObject<Guid>
{
    private SessionId(Guid value) 
        : base(value)
    {
    }

    public static SessionId NewSessionId() => new(Guid.NewGuid());
}