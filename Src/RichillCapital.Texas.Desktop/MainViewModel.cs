using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace RichillCapital.Texas.Desktop;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;

    public ObservableCollection<PlayerModel> Players { get; set; }

    public MainViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        
        Players = new ObservableCollection<PlayerModel>() 
        {
            new PlayerModel
            {
                Id = "1",
                Name = "Jiayee",
            },
            new PlayerModel
            {
                Id = "2",
                Name = "小祐",
            },
            new PlayerModel
            {
                Id = "3",
                Name = "Reno",
            },
            new PlayerModel
            {
                Id = "4",
                Name = "阿貴",
            },
            new PlayerModel
            {
                Id = "5",
                Name = "佳緯",
            },
            new PlayerModel
            {
                Id = "6",
                Name = "Amao",
            },
        };

        BindingOperations.EnableCollectionSynchronization(Players, new());
    }

    [RelayCommand]
    private void OpenAddPlayerDialog()
    {
        _dialogService.ShowDialog<object>();
    }
}

public sealed record PlayerModel
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}