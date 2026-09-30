using Avalonia.Controls;
using Avalonia.Interactivity;
using CatDataViewer.Models;

namespace CatDataViewer.Views;
public partial class ProfileView : Window
{
    Cat cat;
    public ProfileView(Cat c)
    {
        cat = c;
        InitializeComponent();
        DataContext = new CatProfileViewModel(cat);
    }
}