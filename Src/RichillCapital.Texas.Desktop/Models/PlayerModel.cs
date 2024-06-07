using RichillCapital.Texas.Domain;

namespace RichillCapital.Texas.Desktop.Models;

public sealed record PlayerModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal TotalBuyIn { get; init; }
    public required decimal TotalCashOut { get; init; }
}

internal static class PlayerModelMappings
{
    internal static PlayerModel ToModel(this Player player) =>
        new()
        {
            Id = player.Id.Value,
            Name = player.Name,
            TotalBuyIn = player.TotalBuyIn,
            TotalCashOut = player.TotalCashOut,
        };
}