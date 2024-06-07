using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Desktop.Models;
using RichillCapital.Texas.Domain.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace RichillCapital.Texas.Desktop;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly ITexasService _texasService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OpenSessionCommand))]
    [NotifyCanExecuteChangedFor(nameof(OpenBuyInDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(OpenAddPlayerDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(OpenCashOutDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(CloseSessionCommand))]
    public SessionModel? _currentSession;

    public required ObservableCollection<LogModel> Logs { get; set; } = [];

    public MainViewModel(
        IDialogService dialogService,
        ITexasService texasService)
    {
        _dialogService = dialogService;
        _texasService = texasService;
    }

    [RelayCommand(CanExecute = nameof(CanOpenSession))]
    private void OpenSession()
    {
        var result = _texasService.NewSession();

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
    private void OpenBuyInDialog() => _dialogService.ShowDialog<BuyInDialog>();

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void OpenAddPlayerDialog() => _dialogService.ShowDialog<CreatePlayerDialog>();

    [RelayCommand(CanExecute = nameof(CanCashOut))]
    private void OpenCashOutDialog() => _dialogService.ShowDialog<CashOutDialog>();

    [RelayCommand(CanExecute = nameof(CanCloseSession))]
    private void CloseSession()
    {
        var result = _texasService.CloseSession();

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
