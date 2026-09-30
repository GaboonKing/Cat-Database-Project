using CatDataViewer.ViewModels;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.Input;
using CatDataViewer;
using System;

public class SignUpViewModel : ViewModelBase
{
    private string email = "";
    private string pword = "";
    private string error_text = "";

    public Action? _closeAction;
    public IRelayCommand SignUpCmd {get;}
    //public IRelayCommand OpenSignUp{get;}

    public SignUpViewModel(Action closeaction)
    {
        SignUpCmd = new RelayCommand(SignUp);
        _closeAction = closeaction;
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
        _closeAction.Invoke();
    }
    private void SignUp()
    {
        ErrorMsg = "";
        if(email == "" || pword == "")
        {
            ErrorMsg = "Please enter an email and password to sign up.";
            return;
        }
        var repo = new AdopterRepo(Program.Configuration.GetConnectionString("Catabase"));
        
        if(repo.IsEmailTaken(email)) // check if this email has been used
        {
            ErrorMsg = "This email is already connected to an account. Login, or use a different email.";
            return;
        }
        try
        {
            repo.CreateAdopter(email, pword);
            CloseWindow();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
           
        
         
    }
    


}