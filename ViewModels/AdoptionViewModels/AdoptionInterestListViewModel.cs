using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Avalonia.Platform;
using CatDataViewer;
using CatDataViewer.Models;
using CatDataViewer.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

public class AdoptionInterestViewModel : ViewModelBase
{
    
    AdopterRepo adopterRepo = new AdopterRepo(Program.Configuration.GetConnectionString("Catabase"));
    CatRepository catRepo = new CatRepository(Program.Configuration.GetConnectionString("Catabase"));
    public ObservableCollection<AdoptionInterest> UserInterests{get;}
    public ObservableCollection<Cat> InterestedCats{get;}
    public bool HasInterests => UserInterests.Count > 0;
    public bool NoInterests => UserInterests.Count < 1; // need a seperate property for the inverse to connect to a binding
    public RelayCommand<Cat> RemoveInterestCmd{get;} 
    public AdoptionInterestViewModel()
    {
        UserInterests = new ObservableCollection<AdoptionInterest>(adopterRepo.GetInterests(Session.CurrentUser.PersonID));
        InterestedCats = new ObservableCollection<Cat>();
        RemoveInterestCmd = new RelayCommand<Cat>(RemoveCat);
        if(HasInterests)
        {
            foreach(AdoptionInterest ar in UserInterests)
            {
                if(catRepo.DoesCatExist(ar.CatID))
                {
                    Cat c = catRepo.GetCat(ar.CatID);
                    InterestedCats.Add(c);
                }
            }
        }
    }

    public void RemoveCat(Cat c)
    {
        
        InterestedCats.Remove(c);
        foreach(AdoptionInterest ar in UserInterests)
        {
            if(c.Id == ar.CatID)
            {
                ar.DeleteInterest();
                UserInterests.Remove(ar);
                return;
            }
        }
        
    }
   

}