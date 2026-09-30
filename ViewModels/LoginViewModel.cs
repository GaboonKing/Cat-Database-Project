using CatDataViewer.Models;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.Input;
using CatDataViewer;
using CatDataViewer.ViewModels;
using System;
public class LoginViewModel : ViewModelBase
{
    private string email = "";
    private string pword = "";
    private string error_text = "";
    private readonly Action? _closeAction; // closes the window

    public IRelayCommand LoginCmd {get;}
    public IRelayCommand OpenSignUp{get;}
    
    public LoginViewModel(Action closeAction)
    {
        LoginCmd = new RelayCommand(Login);
        _closeAction = closeAction;
    }
    
    public string Email
    {
        get => email;

        set
        {
            email = value;
            OnPropertyChanged();
        }
    }
    public string Pword
    {
        get => pword;

        set
        {
            pword = value;
            OnPropertyChanged();
        }
    }
    public string ErrorMsg
    {
        get => error_text;

        set
        {
            error_text = value;
            OnPropertyChanged();
        }
    }
    private void CloseWindow()
    {
        // Execute the action passed from the view
        _closeAction?.Invoke();
    }
    private void Login()
    {
        ErrorMsg = "";
        if(email == "" || pword == "")
        {
            ErrorMsg = "Please enter your email and password to login.";
            return;
        }
        var repo = new AdopterRepo(Program.Configuration.GetConnectionString("Catabase"));
        try
        {
            Adopter a = repo.FindAdopter(email, pword);
            ErrorMsg = $"{a.F_Name} {a.L_Name}"; // Temporary indicator for a successful login
            // Need to add a way to actually log in
            Session.CurrentUser = a;
            Console.WriteLine(Session.CurrentUser.Email);
            
            CloseWindow();
            
        }
        catch(UnknownIdException)
        {
            ErrorMsg = "Unknown email or password. Please try again.";
        }   
    }



}