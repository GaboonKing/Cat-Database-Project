using Avalonia.Controls;
using Avalonia.Interactivity;
using CatDataViewer.Models;
using CatDataViewer.ViewModels;
using Avalonia;

namespace CatDataViewer.Views.Adoptions;
public partial class AdoptionManager : UserControl
{
    public AdoptionManager()
    {
        InitializeComponent();
        DataContext = new AdoptionManagerViewModel();
    }
}