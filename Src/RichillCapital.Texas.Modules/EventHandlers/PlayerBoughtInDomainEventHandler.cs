using CommunityToolkit.Mvvm.Messaging;
using RichillCapital.Texas.Domain.Events;
using RichillCapital.Texas.Modules.Common;

namespace RichillCapital.Texas.Modules.EventHandlers;

internal sealed class PlayerBoughtInDomainEventHandler(
    IMessenger _messenger) : 
    IDomainEventHandler<PlayerBoughtInDomainEvent>
{
    public Task Handle(
        PlayerBoughtInDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _messenger.Send(domainEvent);

        return Task.CompletedTask;
    }
}
