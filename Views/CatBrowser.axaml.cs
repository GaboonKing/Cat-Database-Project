using Avalonia.Controls;
using Avalonia.Interactivity;
using CatDataViewer.Models;
using CatDataViewer.Views;

namespace CatDataViewer;

public partial class CatBrowserWindow : UserControl
{
    public CatBrowserWindow()
    {
        InitializeComponent();
        DataContext = new CatBrowserViewModel();
    }

    void OpenProfile(object sender, RoutedEventArgs args)
    {
        if (sender is Button b && b.DataContext is Cat cat)
        {
            var profile = new ProfileView(cat); // Intializes the profile window, which in turn calls the view model
            profile.Show();

        }
    }

    
}