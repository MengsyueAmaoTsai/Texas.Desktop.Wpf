using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Desktop.Models;
using RichillCapital.Texas.Domain;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace RichillCapital.Texas.Desktop;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly ITexasService _texasService;

    [ObservableProperty]
    public required SessionModel _currentSession;

    public required ObservableCollection<LogModel> Logs { get; set; }

    public required ObservableCollection<PlayerModel> Players { get; set; }

    public MainViewModel(
        IDialogService dialogService,
        ITexasService texasService)
    {
        _dialogService = dialogService;
        _texasService = texasService;

        Players =
        [
            new() 
            {
                Id = 1,
                Name = "Jiayee",
            },
            new()
            {
                Id = 2,
                Name = "小祐",
            },
            new()
            {
                Id = 3,
                Name = "Reno",
            },
            new()
            {
                Id = 4,
                Name = "阿貴",
            },
            new()
            {
                Id = 5,
                Name = "佳緯",
            },
            new() 
            {
                Id = 6,
                Name = "Amao",
            },
        ];

        BindingOperations.EnableCollectionSynchronization(Players, new());
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

        CurrentSession = CurrentSession with
        {
        };
    }

    private bool CanOpenSession() => true;

    [RelayCommand(CanExecute = nameof(CanCloseSession))]
    private void CloseSession()
    {
        MessageBox.Show("Close session");
    }

    private bool CanCloseSession() => true;

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void OpenAddPlayerDialog()
    {
        MessageBox.Show("Open add player dialog");
    }

    private bool CanAddPlayer() => true;

    [RelayCommand]
    private void BuyIn()
    {
        MessageBox.Show("Buy in");
    }

    [RelayCommand(CanExecute = nameof(CanCashOut))]
    private void CashOut()
    {
        MessageBox.Show("Cash out");
    }

    private bool CanCashOut() => true;
}
