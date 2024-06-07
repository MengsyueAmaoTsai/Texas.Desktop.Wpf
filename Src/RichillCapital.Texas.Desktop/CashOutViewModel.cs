using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.ValueObjects;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class CashOutViewModel(
    ITexasService _texasService,
    IDialogService _dialogService) : 
    ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CashOutCommand))]
    private int _remainingSize = 0;

    private readonly PlayerId PlayerId = PlayerId.From(1).Value;

    [RelayCommand(CanExecute = nameof(CanCashOut))]
    private async Task CashOutAsync()
    {
        var result = await _texasService.CashOutAsync(
            PlayerId, 
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
