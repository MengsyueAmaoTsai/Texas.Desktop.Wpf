namespace RichillCapital.Texas.Desktop.Models;

public sealed record LogModel
{
    public required DateTime Time { get; init; }

    public required string Message { get; init; }
}
