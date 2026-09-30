using System.Dynamic;
using CatDataViewer.ViewModels;
using CatDataViewer.Models;
using CatDataViewer;
using CatDataViewer.Views.Adoptions;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System;


public class AdoptionViewModel : ViewModelBase
{
    CatRepository catRepo = new CatRepository(Program.Configuration.GetConnectionString("Catabase"));
    public ObservableCollection<Cat> OutgoingCats{get;} = new ObservableCollection<Cat>(); // Collection passed to the view
    public string PageTitle {get;} = "FCI Adoptions";
    public string PageInfo {get;} = "Welcome to the Adoption Page. Staff, faculty, and upperclassmen can sign up to the waitlist and potentially be given the oppurtunity to adopt a campus cat.";
    public RelayCommand ChangeView{get;}
    public AdoptionViewModel()
    {
        GetAdoptableCats();
        ChangeView = new RelayCommand(OpenAdoptionManager);
    }
    private void GetAdoptableCats()
    {
        ObservableCollection<Cat> AllFerals = new ObservableCollection<Cat>(catRepo.GetFeral());
        foreach(Cat c in AllFerals)
        {
            if(c.Temperament == "Outgoing")
            {
                OutgoingCats.Add(c);
            }
        }
        
    }
    private void OpenAdoptionManager()
    {
        Session.CurrentView = new AdoptionManager();
    }
}