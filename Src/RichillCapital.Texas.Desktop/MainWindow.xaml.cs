using System.Windows;
using System.Windows.Input;

namespace RichillCapital.Texas.Desktop;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();

        CommandBindings.Add(new CommandBinding(
            ApplicationCommands.Close,
            (sender, e) => Close()));
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Application.Current.Shutdown();
    }
}