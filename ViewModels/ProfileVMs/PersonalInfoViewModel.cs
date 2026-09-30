using Avalonia.Platform;
using CatDataViewer.Models;
using CatDataViewer.Views;
using CatDataViewer.Views.UserProfileViews;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.Input;
using System;



namespace CatDataViewer.ViewModels;

public class PersonalInfoViewModel : ViewModelBase
{

    public Adopter CurrentUser => Session.CurrentUser;
    public IRelayCommand SubmitChangesCmd{get;}

    public ObservableCollection<string> Types {get;} = new() // holds valid types that can be assigned to a user
    {
        "Student", "Faculty", "Staff", "Parent", "Other"
    };

    // Properties
    private string emailDisplay;
    public string EmailDisplay => emailDisplay;

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
    
   // Mapping display fields to DB user fields
   // This dictionary is dynamically created whenever accessed
   Dictionary<string, string> fieldMapping => new()
   {
       {CurrentUser.F_Name, SelectedFName},
       {CurrentUser.L_Name, SelectedLName},
       {CurrentUser.Type, SelectedType},
       {CurrentUser.Email, EmailDisplay}
   };

    public PersonalInfoViewModel()
    {
        selectedFName = CurrentUser.F_Name;
        selectedLName = CurrentUser.L_Name;
        selectedType = CurrentUser.Type;
        emailDisplay = CurrentUser.Email;

        SubmitChangesCmd = new RelayCommand(SubmitChanges);
    }
    
    // Input validation
    private string errorMsg = "";
    public string ErrorMsg => errorMsg;
    void SubmitChanges()
    {
        Console.WriteLine("SubmitChanges Called");
        errorMsg = ""; // reset the error message incase of multiple attempts
        foreach(KeyValuePair<string, string> kvp in fieldMapping)
        {
            // Only displays error if a space that hasn't already been filled is left empty
            if(string.IsNullOrWhiteSpace(kvp.Key) && string.IsNullOrWhiteSpace(kvp.Value))
            {
                errorMsg = "Please ensure all fields are filled out";
                return;
            }   
        }
        AdopterRepo ar = new AdopterRepo(Program.Configuration.GetConnectionString("Catabase"));
        ar.UpdatePersonalDetails(SelectedFName, SelectedLName, SelectedType, EmailDisplay, CurrentUser.PersonID);
        
       
        
    }
}