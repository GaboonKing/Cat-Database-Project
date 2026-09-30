using CatDataViewer.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
namespace CatDataViewer.ViewModels;

public class AccountManagerViewModel : ViewModelBase
{
    public Adopter CurrentUser => Session.CurrentUser;

    public IRelayCommand LogoutCmd{get;}
    public IRelayCommand DeleteAccount{get;}
    public IRelayCommand ChangePassword{get;}

    private string pword;
    public string Pword => pword;

    public AccountManagerViewModel()
    {
        pword = CurrentUser.Password;
        LogoutCmd = new RelayCommand(Logout);
        
    }

    public void Logout()
    {
        Session.CurrentUser = null;
    }
    // TODO: Add alternative layout that appears after the user logs out
}