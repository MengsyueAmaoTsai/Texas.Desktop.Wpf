using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public class PlayerId : SingleValueObject<int>
{
    private PlayerId(int value) 
        : base(value)
    {
    }

    public static Result<PlayerId> From(int value) => value
        .ToResult()
        .Then(id => new PlayerId(id));
}