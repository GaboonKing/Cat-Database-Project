using Avalonia.Controls;
using Avalonia.Interactivity;
namespace CatDataViewer.Views;

public partial class SignUpWindow : Window
{
    MainWindow mw = new MainWindow();
    public SignUpWindow(MainWindow m)
    {
        InitializeComponent();
        DataContext = new SignUpViewModel(this.Close);
        mw = m;
    }

    void OpenLoginWindow(object? sender, RoutedEventArgs args)
    {
        var LWindow = new LoginWindow(mw);
        LWindow.ShowDialog(mw);
        this.Close();
    }

    
}