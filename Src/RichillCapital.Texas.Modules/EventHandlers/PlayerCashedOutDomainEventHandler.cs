using CommunityToolkit.Mvvm.Messaging;
using RichillCapital.Texas.Domain.Events;
using RichillCapital.Texas.Modules.Common;

namespace RichillCapital.Texas.Modules.EventHandlers;

internal sealed class PlayerCashedOutDomainEventHandler(IMessenger _messenger) : 
    IDomainEventHandler<PlayerCashedOutDomainEvent>
{
    public Task Handle(
        PlayerCashedOutDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _messenger.Send(domainEvent);

        return Task.CompletedTask;
    }
}
