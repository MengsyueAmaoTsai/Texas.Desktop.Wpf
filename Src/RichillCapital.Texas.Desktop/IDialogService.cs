namespace RichillCapital.Texas.Desktop;

public interface IDialogService 
{
    void ShowDialog<TDialog>();
    //void ShowDialog<TDialog, TViewModel>(TViewModel viewModel) where TDialog : IDialog;
    //void ShowDialog<TDialog>(Action<TDialog> configureDialog) where TDialog : IDialog;
    //void ShowDialog<TDialog, TViewModel>(TViewModel viewModel, Action<TDialog> configureDialog) where TDialog : IDialog;
    //Task ShowDialogAsync<TDialog>() where TDialog : IDialog;
    //Task ShowDialogAsync<TDialog, TViewModel>(TViewModel viewModel) where TDialog : IDialog;
    //Task ShowDialogAsync<TDialog>(Action<TDialog> configureDialog) where TDialog : IDialog;
    //Task ShowDialogAsync<TDialog, TViewModel>(TViewModel viewModel, Action<TDialog> configureDialog) where TDialog : IDialog;
    //Task<TResult> ShowDialogAsync<TDialog, TResult>() where TDialog : IDialog<TResult>;
    //Task<TResult> ShowDialogAsync<TDialog, TViewModel, TResult>(TViewModel viewModel) where TDialog : IDialog<TResult>;
    //Task<TResult> ShowDialogAsync<TDialog, TResult>(Action<TDialog> configureDialog) where TDialog : IDialog<TResult>;
    //Task<TResult> ShowDialogAsync<TDialog, TViewModel, TResult>(TViewModel viewModel, Action<TDialog> configureDialog) where TDialog : IDialog<TResult>;
    //Task ShowDialogAsync<TDialog, TViewModel>(TViewModel viewModel, Action<TDialog, Task> configureDialog) where TDialog : IDialog;
    //Task<TResult> ShowDialogAsync<TDialog, TViewModel, TResult>(TViewModel viewModel, Action<TDialog, Task<TResult>> configureDialog) where TDialog : IDialog<TResult>;
    //Task ShowDialogAsync<TDialog>(Action<TDialog, Task> configureDialog) where TDialog : IDialog;
    //Task<TResult> ShowDialogAsync<TDialog, TResult>(Action<TDialog, Task<TResult>> configureDialog) where TDialog : IDialog<TResult>;
    //Task ShowDialogAsync<TDialog, TViewModel>(TViewModel viewModel, Action<TDialog, Task> configureDialog) where TDialog : IDialog;
    //Task<TResult> ShowDialogAsync<TDialog, TViewModel, TResult>(TViewModel viewModel, Action<TDialog, Task<TResult>> configureDialog) where TDialog : IDialog<TResult>;
    //Task ShowDialogAsync<TDialog>(Action<TDialog, Task> configureDialog) where TDialog : IDialog;
    //Task<TResult> Show
}

internal sealed class DialogService : IDialogService
{
    public void ShowDialog<TDialog>() => throw new NotImplementedException();
}