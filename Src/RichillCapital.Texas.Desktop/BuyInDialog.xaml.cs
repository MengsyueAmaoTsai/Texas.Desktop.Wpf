using System.Windows;

namespace RichillCapital.Texas.Desktop;

public sealed partial class BuyInDialog : Window
{
    public BuyInDialog(BuyInViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
