using Avalonia.Automation.Peers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatDataViewer.ViewModels;
using CatDataViewer.Models;

namespace CatDataViewer.Views.UserProfileViews;

public partial class AccountManager : UserControl
{
    
    public AccountManager()
    {
        InitializeComponent();
        DataContext = new AccountManagerViewModel();
    }

    
}