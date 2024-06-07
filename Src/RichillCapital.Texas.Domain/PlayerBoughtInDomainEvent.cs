﻿namespace RichillCapital.Texas.Domain;

internal sealed record PlayerBoughtInDomainEvent
{
    public required DateTime Time { get; init; }
    public required string PlayerId { get; init; }
    public required string PlayerName { get; init; }
    public required int Groups { get; init; }
    public required int TotalValue { get; init; }

    public string ToAlertMessage() => $"{Time:MM-dd HH:mm:ss} - 玩家 {PlayerName} Buy-In {Groups} 組 ({TotalValue})";
}

internal sealed record PlayerCashedOutDomainEvent
{
    public required DateTime Time { get; init; }
    public required string PlayerId { get; init; }
    public required string PlayerName { get; init; }
    public required int TotalValue { get; init; }

    public string ToAlertMessage() => $"{Time:MM-dd HH:mm:ss} - 玩家 {PlayerName} Cash-Out ({TotalValue})";
}