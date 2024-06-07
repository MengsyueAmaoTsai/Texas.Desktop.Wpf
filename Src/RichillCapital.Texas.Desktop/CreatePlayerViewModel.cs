using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichillCapital.Texas.Domain;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class CreatePlayerViewModel(
    ITexasService _texasService,
    IDialogService _dialogService) : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string _name = string.Empty;

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void AddPlayer()
    {
        var result = _texasService.AddPlayer(Name);
        
        if (result.IsFailure)
        {
            MessageBox.Show($"{result.Error}");
            return;
        }

        MessageBox.Show($"Player {Name} added successfully");
    }

    private bool CanAddPlayer() => !string.IsNullOrEmpty(Name);

    [RelayCommand]
    private void Cancel() => _dialogService.CloseDialog<CreatePlayerDialog>();
}
