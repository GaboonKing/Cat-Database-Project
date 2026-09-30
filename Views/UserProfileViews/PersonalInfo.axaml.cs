using Avalonia.Controls;
using CatDataViewer.ViewModels;
namespace CatDataViewer.Views.UserProfileViews;
public partial class PersonalInfo : UserControl
{
    public PersonalInfo()
    {
        InitializeComponent();
        DataContext = new PersonalInfoViewModel();
    }
    
}