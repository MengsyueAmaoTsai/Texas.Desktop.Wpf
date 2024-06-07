using RichillCapital.SharedKernel;

namespace RichillCapital.Texas.Domain.Common.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTimeOffset OccurredTime => DateTimeOffset.UtcNow;
}
