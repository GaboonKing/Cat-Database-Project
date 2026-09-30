using System.ComponentModel;
using System.Runtime.CompilerServices;
using CatDataViewer.Models;

namespace CatDataViewer.ViewModels;
// Base class that is needed for the UI to dynamically react to user text input
public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected SessionManager Session {get;}
    
    protected ViewModelBase()
    {
        Session = AppSession.Instance;
    }

    protected void OnPropertyChanged(
        [CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(name));
    }
}

public static class AppSession
{
    public static SessionManager Instance { get; } =
        new SessionManager();
}