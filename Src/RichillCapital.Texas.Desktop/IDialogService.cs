using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace RichillCapital.Texas.Desktop;

public interface IDialogService 
{
    void ShowDialog<TDialog>() where TDialog : Window;
    void CloseDialog<TDialog>() where TDialog : Window;
}

internal sealed class DialogService(
    IServiceProvider _serviceProvider) : 
    IDialogService
{
    public void CloseDialog<TDialog>() where TDialog : Window
    {
        var dialog = App.Current.Windows.OfType<TDialog>().FirstOrDefault();

        if (dialog is null)
        {
            return;
        }

        dialog.Close();
    }

    public void ShowDialog<TDialog>() 
        where TDialog : Window => 
        _serviceProvider.GetRequiredService<TDialog>().ShowDialog();
}