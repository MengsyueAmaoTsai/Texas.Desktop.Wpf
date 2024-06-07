using RichillCapital.Texas.Domain;

namespace RichillCapital.Texas.Desktop.Models;

public sealed record SessionModel
{
    public required Guid Id { get; init; }
}

internal static class SessionModelMappings
{
    internal static SessionModel ToModel(this Session session) =>
        new()
        {
            Id = session.Id.Value,
        };
}