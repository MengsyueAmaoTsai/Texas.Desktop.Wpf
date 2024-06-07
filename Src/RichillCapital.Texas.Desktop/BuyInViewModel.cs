using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.ValueObjects;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class BuyInViewModel : 
    ObservableObject
{
    private readonly ITexasService _texasService;
    private readonly IDialogService _dialogService;
    private readonly PlayerId _playerId;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyInCommand))]
    private int _units = 1;

    public BuyInViewModel(
        ITexasService texasService,
        IDialogService dialogService,
        MainViewModel mainViewModel)
    {
        _texasService = texasService;
        _dialogService = dialogService;
        _playerId = PlayerId.From(mainViewModel.SelectedPlayer.Id).Value;
    }

    [RelayCommand(CanExecute = nameof(CanBuyIn))]
    private async Task BuyInAsync()
    {
        var result = await _texasService.BuyInAsync(
            _playerId,
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
