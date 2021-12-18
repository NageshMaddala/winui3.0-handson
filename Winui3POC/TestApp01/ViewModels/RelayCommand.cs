using System.Windows.Input;
using Microsoft.UI.Xaml.Input;
namespace TestApp01.ViewModels;
using System;

public class RelayCommand : ICommand
{
    private readonly Action action;
    private readonly Func<bool> canExecute;

    public RelayCommand(Action action)
        : this(action, null)
    {
    }

    public RelayCommand(Action action, Func<bool> canExecute)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        this.action = action;
        this.canExecute = canExecute;
    }

    event EventHandler ICommand.CanExecuteChanged
    {
        add
        {
        }

        remove
        {
        }
    }

    public bool CanExecute(object parameter) => canExecute == null || canExecute();

    public void Execute(object parameter) => action();

    public event EventHandler<object> CanExecuteChanged;

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}