using CommunityToolkit.Mvvm.Messaging;
using RichillCapital.Texas.Domain.Events;
using RichillCapital.Texas.Modules.Common;

namespace RichillCapital.Texas.Modules.EventHandlers;

internal sealed class SessionOpenedDomainEventHandler(IMessenger _messenger) : 
    IDomainEventHandler<SessionOpenedDomainEvent>
{
    public Task Handle(
        SessionOpenedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _messenger.Send(domainEvent);

        return Task.CompletedTask;
    }
}
