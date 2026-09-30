using System;
using Avalonia.Controls;

using Avalonia.Interactivity;
using CatDataViewer.ViewModels;

namespace CatDataViewer.Views.UserProfileViews;

public partial class UserProfileWindow : Window
{
    public UserProfileWindow()
    {
        InitializeComponent();
        DataContext = new UserProfileViewModel();

    }
        
}