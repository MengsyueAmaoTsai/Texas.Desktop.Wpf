using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RichillCapital.Texas.Desktop.Models;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.Events;
using System.Collections.ObjectModel;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class MainViewModel : ObservableRecipient
{
    private readonly IDialogService _dialogService;
    private readonly ITexasService _texasService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OpenSessionCommand))]
    [NotifyCanExecuteChangedFor(nameof(OpenBuyInDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(OpenAddPlayerDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(OpenCashOutDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(CloseSessionCommand))]
    public TexasSessionModel? _currentSession;

    [ObservableProperty]
    public required PlayerModel _selectedPlayer;

    public required ObservableCollection<PlayerModel> Players { get; set; } = [];

    public MainViewModel(
        IDialogService dialogService,
        ITexasService texasService,
        IMessenger _messenger)
    {
        _dialogService = dialogService;
        _texasService = texasService;

        _messenger.Register<SessionOpenedDomainEvent>(this, (_, _) => UpdateUI());
        _messenger.Register<SessionClosedDomainEvent>(this, (_, _) => UpdateUI());
        _messenger.Register<PlayerBoughtInDomainEvent>(this, (_, _) => UpdateUI());
        _messenger.Register<PlayerCashedOutDomainEvent>(this, (_, _) => UpdateUI());
        _messenger.Register<PlayerJoinedDomainEvent>(this, (_, _) => UpdateUI());
    }

    private void UpdateUI()
    {
        UpdateSession();
        UpdatePlayers();
    }

    private void UpdatePlayers()
    {
        Players.Clear();

        var players = _texasService.GetPlayers();

        foreach (var player in players)
        {
            Players.Add(player.ToModel());
        }
    }

    private void UpdateSession()
    {
        var session = _texasService.GetSession();
        CurrentSession = session.IsNull ? null : session.Value.ToModel();
    }

    [RelayCommand(CanExecute = nameof(CanOpenSession))]
    private async Task OpenSessionAsync()
    {
        var result = await _texasService.NewSessionAsync();

        if (result.IsFailure)
        {
            MessageBox.Show($"{result.Error}");
        }

        if (CurrentSession is null)
        {
            CurrentSession = result.Value.ToModel();
        }
    }

    [RelayCommand(CanExecute = nameof(CanBuyIn))]
    private void OpenBuyInDialog()
    {
        if (SelectedPlayer is null)
        {
            return;
        }

        _dialogService.ShowDialog<BuyInDialog>();
    }

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void OpenAddPlayerDialog() => _dialogService.ShowDialog<CreatePlayerDialog>();


    [RelayCommand(CanExecute = nameof(CanCashOut))]
    private void OpenCashOutDialog()
    {
        if (SelectedPlayer is null)
        {
            return;
        }

        _dialogService.ShowDialog<CashOutDialog>();
    }

    [RelayCommand(CanExecute = nameof(CanCloseSession))]
    private async Task CloseSessionAsync()
    {
        var result = await _texasService.CloseSessionAsync();

        if (result.IsFailure)
        {
            MessageBox.Show($"{result.Error}");
        }

        CurrentSession = null;
    }

    private bool CanOpenSession() => CurrentSession is null;
    private bool CanAddPlayer() => CurrentSession is not null;
    private bool CanBuyIn() => true;
    private bool CanCashOut() => true;
    private bool CanCloseSession() => CurrentSession is not null;
}
