using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.Events;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class CreatePlayerViewModel : ObservableObject
{
    private readonly ITexasService _texasService;
    private readonly IDialogService _dialogService;

    public CreatePlayerViewModel(
        ITexasService texasService,
        IDialogService dialogService,
        IMessenger _messenger)
    {
        _texasService = texasService;
        _dialogService = dialogService;

        _messenger.Register<PlayerJoinedDomainEvent>(this, (_, message) =>
        {
            PlayerName = string.Empty;
        });
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string _playerName = string.Empty;

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private async Task AddPlayerAsync()
    {
        var result = await _texasService.AddPlayerAsync(PlayerName);
        
        if (result.IsFailure)
        {
            MessageBox.Show($"{result.Error}");
            return;
        }
    }

    private bool CanAddPlayer() => !string.IsNullOrWhiteSpace(PlayerName);

    [RelayCommand]
    private void Cancel() => _dialogService.CloseDialog<CreatePlayerDialog>();
}
