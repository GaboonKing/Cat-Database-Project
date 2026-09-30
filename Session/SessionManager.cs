
using CommunityToolkit.Mvvm.ComponentModel;
using CatDataViewer.Models;
using System;
public class SessionManager : ObservableObject
{
    private Adopter? currentUser;
    private Object? currentView;
    public Adopter? CurrentUser
    {
        get => currentUser;
        set
        {
            SetProperty(ref currentUser, value); // Intutively calls OnPropertyChanged for CurrentUser
            OnPropertyChanged(nameof(IsLoggedIn)); // nameof is used in place of hard coding the properties name as a string
            OnPropertyChanged(nameof(IsLoggedOut));
        }
    }
    public Object CurrentView 
    {
        get => currentView;
        set => SetProperty(ref currentView, value);
        
    }
    public bool IsLoggedIn => CurrentUser != null;
    public bool IsLoggedOut => CurrentUser == null;
}