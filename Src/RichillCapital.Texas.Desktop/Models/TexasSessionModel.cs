using RichillCapital.Texas.Domain.Entities;

namespace RichillCapital.Texas.Desktop.Models;

public sealed record TexasSessionModel
{
    public required Guid Id { get; init; }

    public required IEnumerable<PlayerModel> Players { get; init; }
    
    public required int TotalBuyIn { get; init; }

    public required int TotalCashOut { get; init; }

    public int RemainingSize { get; init; }
}

internal static class SessionModelMappings
{
    internal static TexasSessionModel ToModel(this Session session) =>
        new()
        {
            Id = session.Id.Value,
            Players = session.Players.Select(player => player.ToModel()),
            TotalBuyIn = session.TotalBuyIn,
            TotalCashOut = session.TotalCashOut,
            RemainingSize = session.TotalBuyIn - session.TotalCashOut,
        };
}