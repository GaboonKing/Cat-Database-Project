using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatDataViewer.Views.UserProfileViews;


namespace CatDataViewer.Views;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        Debug.WriteLine("test");
    }

    void OpenLoginWindow(object sender, RoutedEventArgs args)
    {
        if (sender is Button b)
        {
            var login = new LoginWindow(this); // Intializes the profile window, which in turn calls the view model
            login.ShowDialog(this); // freezes the MainWindow while displaying the profile popup

        }
    }

    private void OpenUserProfile(object? sender, RoutedEventArgs e)
    {
        if(sender is Button b)
        {
            var profile = new UserProfileWindow();
            profile.ShowDialog(this);
        }
    }
}