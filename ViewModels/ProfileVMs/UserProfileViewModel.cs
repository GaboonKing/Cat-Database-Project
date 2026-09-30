using CatDataViewer.Models;
using CatDataViewer.Views.UserProfileViews;
using CatDataViewer.Views;

using CommunityToolkit.Mvvm.Input;
using System;


namespace CatDataViewer.ViewModels;

public class UserProfileViewModel : ViewModelBase
{
    public Adopter? CurrentUser => Session.CurrentUser;
    // public IRelayCommand ChangeStatusCmd {get;}

    // Relay Commands
    public IRelayCommand OpenPersonalInfoCmd {get;} // Reverts back to the starting frame
    public IRelayCommand OpenContactInfoCmd {get;}
    public IRelayCommand OpenAccountMngrCmd {get;}

    private object? currentSection;

    // User attributes


    private string? selectedFName; // holds the users selected first name in the UI, exists a bridge between the UI and DB
    public string SelectedFName
    {
        get
        {
            return selectedFName;
        }
        set
        {
            selectedFName = value;
            OnPropertyChanged();
        }
    }
    private string? selectedLName;
    public string SelectedLName
    {
        get
        {
            return selectedLName;
        }
        set
        {
            selectedLName = value;
            OnPropertyChanged();
        }
    }
    private string selectedType;
    public string SelectedType
    {
        get
        {
            return selectedType;
        }
        set
        {
            selectedType = value;
            OnPropertyChanged();
        }
    }    

    public object CurrentSection
    {
        get => currentSection;
        set
        {
            currentSection = value;
            OnPropertyChanged();
            
        }
    }
    public UserProfileViewModel()
    {
     
        CurrentSection = new PersonalInfo();
        OpenPersonalInfoCmd = new RelayCommand(SwitchToPersonalInfo);
        OpenAccountMngrCmd = new RelayCommand(SwitchToAccountMnger);
       
        
    }

    // Methods
    private void SwitchToPersonalInfo()
    {
        CurrentSection = new PersonalInfo();
        
    }
    private void SwitchToAccountMnger()
    {
        CurrentSection = new AccountManager();
    }
    
    
}