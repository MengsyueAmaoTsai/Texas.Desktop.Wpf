using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.ValueObjects;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class BuyInViewModel(
    ITexasService _texasService,
    IDialogService _dialogService) : 
    ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyInCommand))]
    private int _units = 1;

    private readonly PlayerId PlayerId = PlayerId.From(1).Value;

    [RelayCommand(CanExecute = nameof(CanBuyIn))]
    private async Task BuyInAsync()
    {
        var result = await _texasService.BuyInAsync(
            PlayerId, 
            Units, 
            default);

        if (result.IsFailure)
        {
            MessageBox.Show($"{result.Error}");
            return;
        }
    }

    private bool CanBuyIn() => Units > 0;

    [RelayCommand]
    private void Cancel() => _dialogService.CloseDialog<BuyInDialog>();
}
