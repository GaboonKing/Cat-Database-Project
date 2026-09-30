using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatDataViewer.Models;

namespace CatDataViewer.Views.Adoptions;

public partial class AdoptionPage : UserControl
{
    //public string PageTitle{get;} = "FCI Adoptions";

    public AdoptionPage()
    {
        InitializeComponent();
        DataContext = new AdoptionViewModel();
    }

    void OpenProfile(object sender, RoutedEventArgs args)
    {
        if(sender is Button b && b.DataContext is Cat c)
        {
            Console.WriteLine("Opening profile of {0}", c.Name);
            var p = new ProfileView(c);
            p.Show();
        }
        else if(DataContext is not Cat)
        {
            Console.WriteLine("Data Context is of incorrect type");
        }
    }

    
}