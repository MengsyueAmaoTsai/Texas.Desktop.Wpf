using CommunityToolkit.Mvvm.Messaging;
using RichillCapital.Texas.Domain.Events;
using RichillCapital.Texas.Modules.Common;

namespace RichillCapital.Texas.Modules.EventHandlers;

internal sealed class PlayerJoinedDomainEventHandler(
    IMessenger _messenger) : 
    IDomainEventHandler<PlayerJoinedDomainEvent>
{
    public Task Handle(
        PlayerJoinedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _messenger.Send(domainEvent);

        return Task.CompletedTask;
    }
}