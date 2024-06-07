using MediatR;
using RichillCapital.SharedKernel;

namespace RichillCapital.Texas.Modules.Common;
internal interface IDomainEventHandler<TDomainEvent> :
    INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    new Task Handle(
        TDomainEvent domainEvent,
        CancellationToken cancellationToken);
}