using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestApp01.ViewModels;

public class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T originalValue, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (Equals(originalValue, newValue))
        {
            return false;
        }

        originalValue = newValue;
        OnPropertyChanged(propertyName);

        return true;
    }
}