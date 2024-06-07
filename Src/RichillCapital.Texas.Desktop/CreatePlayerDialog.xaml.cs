using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class CreatePlayerDialog : Window
{
    public CreatePlayerDialog(CreatePlayerViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
