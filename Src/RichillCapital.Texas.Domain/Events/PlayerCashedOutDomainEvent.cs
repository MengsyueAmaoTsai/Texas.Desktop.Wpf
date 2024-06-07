using RichillCapital.Texas.Domain.Common.Events;

namespace RichillCapital.Texas.Domain.Events;

internal sealed record PlayerCashedOutDomainEvent : DomainEvent
{
    public required DateTime Time { get; init; }
    public required string PlayerId { get; init; }
    public required string PlayerName { get; init; }
    public required int TotalValue { get; init; }

    public string ToAlertMessage() => $"{Time:MM-dd HH:mm:ss} - 玩家 {PlayerName} Cash-Out ({TotalValue})";
}