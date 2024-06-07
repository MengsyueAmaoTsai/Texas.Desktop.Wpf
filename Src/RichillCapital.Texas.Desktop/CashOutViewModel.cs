using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain.Common;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class CashOutViewModel(
    ITexasService _texasService,
    IDialogService _dialogService) : 
    ObservableObject
{
    [ObservableProperty]
    private int _remainingSize = 0;

    [RelayCommand]
    private void CashOut()
    {
    }

    private bool CanCashOut()
    {
        return true;
    }

    [RelayCommand]
    private void Cancel() => _dialogService.CloseDialog<CreatePlayerDialog>();
}
