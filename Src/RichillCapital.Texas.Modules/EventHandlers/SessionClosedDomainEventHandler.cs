using CommunityToolkit.Mvvm.Messaging;
using RichillCapital.Texas.Domain.Events;
using RichillCapital.Texas.Modules.Common;

namespace RichillCapital.Texas.Modules.EventHandlers;

internal sealed class SessionClosedDomainEventHandler(IMessenger _messenger) : 
    IDomainEventHandler<SessionClosedDomainEvent>
{
    public Task Handle(
        SessionClosedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _messenger.Send(domainEvent);

        return Task.CompletedTask;
    }
}