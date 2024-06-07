using System.Windows;

namespace RichillCapital.Texas.Desktop;

public partial class CashOutDialog : Window
{
    public CashOutDialog(CashOutViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
