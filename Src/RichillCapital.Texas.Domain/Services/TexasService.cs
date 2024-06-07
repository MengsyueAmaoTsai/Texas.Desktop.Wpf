using MediatR;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.Entities;
using RichillCapital.Texas.Domain.Errors;
using RichillCapital.Texas.Domain.Events;
using RichillCapital.Texas.Domain.ValueObjects;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RichillCapital.Texas.Domain.UnitTests")]

namespace RichillCapital.Texas.Domain.Services;

internal class TexasService(
    IMediator _medaitor) : 
    ITexasService
{
    internal const int DefaultBuyInSize = 1000;

    private Maybe<Session> CurrentSession { get; set; }

    public Maybe<Session> GetCurrentSession() => CurrentSession;

    public async Task<Result<Player>> AddPlayerAsync(string name, CancellationToken cancellationToken = default)
    {
        if (CurrentSession.IsNull)
        {
            return DomainErrors
                .SessionNotOpen
                .ToResult<Player>();
        }

        var idResult = PlayerId.From(CurrentSession.Value.Players.Count + 1);

        if (idResult.IsFailure)
        {
            return idResult.Error.ToResult<Player>();
        }

        var errorOrPlayer = Player.Create(idResult.Value, name);

        if (errorOrPlayer.HasError)
        {
            return errorOrPlayer.Errors.First()
                .ToResult<Player>();
        }

        var addPlayerResult = CurrentSession.Value.AddPlayer(errorOrPlayer.Value);

        if (addPlayerResult.IsFailure)
        {
            return addPlayerResult.Error.ToResult<Player>();
        }

        await _medaitor.Publish(
            new PlayerJoinedDomainEvent
            {
            }, 
            cancellationToken);

        return errorOrPlayer.Value.ToResult();
    }

    public async Task<Result> BuyInAsync(
        PlayerId playerId, 
        int groups = 1,
        CancellationToken cancellationToken = default)
    {
        var maybePlayer = GetPlayer(playerId);

        if (maybePlayer.IsNull)
        {
            return DomainErrors
                .PlayerNotFound(playerId)
                .ToResult();
        }

        var player = maybePlayer.Value;

        player.BuyIn(groups * CurrentSession.Value.BuyInSize);

        await _medaitor.Publish(
            new PlayerBoughtInDomainEvent
            {
            }, 
            cancellationToken);

        return Result.Success;
    }

    public async Task<Result> CashOutAsync(
        PlayerId playerId,
        int remainingSize,
        CancellationToken cancellationToken = default)
    {
        var maybePlayer = GetPlayer(playerId);

        if (maybePlayer.IsNull)
        {
            return DomainErrors
                .PlayerNotFound(playerId)
                .ToResult();
        }

        var cashOutResult = maybePlayer.Value.CashOut(remainingSize);

        if (cashOutResult.IsFailure)
        {
            return cashOutResult.Error.ToResult();
        }

        await _medaitor.Publish(
            new PlayerCashedOutDomainEvent
            {
            },
            cancellationToken);
        return cashOutResult;
    }

    public async Task<Result> CloseSessionAsync()
    {
        if (CurrentSession.IsNull)
        {
            return DomainErrors
                 .SessionNotOpen
                 .ToResult();
        }

        CurrentSession = Maybe<Session>.Null;

        await _medaitor.Publish(
            new SessionClosedDomainEvent(), 
            CancellationToken.None);

        return Result.Success;
    }

    public async Task<Result<Session>> NewSessionAsync(
        int buyInSize = DefaultBuyInSize,
        CancellationToken cancellationToken = default)
    {
        if (CurrentSession.HasValue)
        {
            return DomainErrors
                .SessionAlreadyExists(CurrentSession.Value.Id)
                .ToResult<Session>();
        }

        var errorOrSession = Session.New(buyInSize);

        if (errorOrSession.HasError)
        {
            return errorOrSession
                .Errors.First()
                .ToResult<Session>();
        }

        var newSession = errorOrSession.Value;

        CurrentSession = newSession.ToMaybe();

        await _medaitor.Publish(
            new SessionOpenedDomainEvent(), 
            cancellationToken);

        return newSession.ToResult();
    }

    public int GetPlayerCount() => CurrentSession.HasValue ?
        CurrentSession.Value.Players.Count : 0;

    public int GetTotalBuyIn() => CurrentSession.HasValue ?
        CurrentSession.Value.Players.Sum(player => player.TotalBuyIn) : 0;

    private Maybe<Player> GetPlayer(PlayerId id)
    {
        var player = CurrentSession.Value.Players
            .FirstOrDefault(p => p.Id == id);

        return Maybe<Player>.With(player!);
    }

    public IEnumerable<Player> GetPlayers() => CurrentSession.HasValue ? 
        CurrentSession.Value.Players.ToList() : 
        [];

    public Maybe<Session> GetSession() => CurrentSession;
}
