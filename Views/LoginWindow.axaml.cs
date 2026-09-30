using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Configuration;
using CatDataViewer.Models;
using Tmds.DBus.Protocol;

namespace CatDataViewer.Views;

public partial class LoginWindow : Window
{
    MainWindow mw;
    public LoginWindow(MainWindow m) // pass in the main window so we can use it as a param for show dialog
    {
        InitializeComponent();
        mw = m;
        DataContext = new LoginViewModel(this.Close);
    }

    void OpenSignUpWindow(object sender, RoutedEventArgs args)
    {
        if(sender is Button b)
        {
            var SUWindow = new SignUpWindow(mw);
            SUWindow.ShowDialog(mw);
            this.Close();
        }
    }
    
}