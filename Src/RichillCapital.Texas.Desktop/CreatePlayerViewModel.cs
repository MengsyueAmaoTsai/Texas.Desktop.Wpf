using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain.Common;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class CreatePlayerViewModel(
    ITexasService _texasService,
    IDialogService _dialogService) : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string _playerName = string.Empty;

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void AddPlayer()
    {
        var result = _texasService.AddPlayer(PlayerName);
        
        if (result.IsFailure)
        {
            MessageBox.Show($"{result.Error}");
            return;
        }

        MessageBox.Show($"Player {PlayerName} added successfully");
    }

    private bool CanAddPlayer() => !string.IsNullOrWhiteSpace(PlayerName);

    [RelayCommand]
    private void Cancel() => _dialogService.CloseDialog<CreatePlayerDialog>();
}
