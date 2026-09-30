using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Controls;
using System;
using CatDataViewer.ViewModels;

namespace CatDataViewer.Views.ColonyMap;
public partial class Map : UserControl
{
    public Map()
    {
        InitializeComponent();
        DataContext = new MapViewModel();
    }
    
}