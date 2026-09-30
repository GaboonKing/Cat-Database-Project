using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using CatDataViewer.Models;
using CatDataViewer.ViewModels;
using CatDataViewer.Views;

namespace CatDataViewer.Views.ColonyMap;

public partial class ZoneViewer : UserControl
{
    public ZoneViewer(Zone z)
    {
        InitializeComponent();
        DataContext = new SelectedZoneVM(z);
    }
    void CloseControl(object sender, RoutedEventArgs args)
    {
        return;
    }
}