using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.ValueObjects;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class CashOutViewModel : 
    ObservableObject
{
    private readonly ITexasService _texasService;
    private readonly IDialogService _dialogService;
    private readonly PlayerId _playerId;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CashOutCommand))]
    private int _remainingSize = 0;

    public CashOutViewModel(
        ITexasService texasService,
        IDialogService dialogService,
        MainViewModel mainViewModel)
    {
        _texasService = texasService;
        _dialogService = dialogService;
        _playerId = PlayerId.From(mainViewModel.SelectedPlayer.Id).Value;
    }

    [RelayCommand(CanExecute = nameof(CanCashOut))]
    private async Task CashOutAsync()
    {
        var result = await _texasService.CashOutAsync(
            _playerId, 
            RemainingSize, 
            default);

        if (result.IsFailure)
        {
            MessageBox.Show($"{result.Error}");
            return;
        }
    }

    private bool CanCashOut() => RemainingSize >= 0;

    [RelayCommand]
    private void Cancel() => _dialogService.CloseDialog<CashOutDialog>();
}
