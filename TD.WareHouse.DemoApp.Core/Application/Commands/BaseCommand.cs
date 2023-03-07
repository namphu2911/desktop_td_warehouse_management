using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.Commands;
public abstract class BaseCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter) => true;
    public abstract void Execute(object? parameter);

    protected void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
