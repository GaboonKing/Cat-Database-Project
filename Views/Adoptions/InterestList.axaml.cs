using Avalonia.Controls;
using Avalonia.Interactivity;
using CatDataViewer.Models;
namespace CatDataViewer.Views.Adoptions;
public partial class InterestList : UserControl
{
    public InterestList()
    {
        InitializeComponent();
        DataContext = new AdoptionInterestViewModel();
    }

    void OpenProfile(object sender, RoutedEventArgs args)
    {
        if(sender is Button b && b.DataContext is Cat c)
        {
            var p = new ProfileView(c);
            p.Show();
        }
    }
}