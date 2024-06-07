using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain;

namespace RichillCapital.Texas.Desktop;

public sealed partial class BuyInViewModel(
    ITexasService _texasService,
    IDialogService _dialogService) : ObservableObject
{
    [ObservableProperty]
    private int _units = 1;

    [RelayCommand]
    private void BuyIn()
    {
    }

    private bool CanBuyIn()
    {
        return true;
    }

    [RelayCommand]
    private void Cancel() => _dialogService.CloseDialog<CreatePlayerDialog>();
}
