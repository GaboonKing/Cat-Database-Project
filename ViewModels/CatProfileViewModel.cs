using Avalonia.Media.Imaging;
using CatDataViewer;
using CatDataViewer.Models;
using CatDataViewer.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

public class CatProfileViewModel : ViewModelBase
{
    public SessionManager AppSession => Session;
    public Cat CurrentCat{get;}
    public string Name => CurrentCat.Name;
    public char Gender => CurrentCat.Gender;
    public string Bio => CurrentCat.Bio;
    public Bitmap ImagePath => CurrentCat.RawImagePath;
    public bool IsAdoptable => CurrentCat.Temperament == "Outgoing";
    ZoneRepo z = ZoneRepo.getInstance(Program.Configuration.GetConnectionString("Catabase"));
    
    public string Location => ZoneRepo.GetName(CurrentCat.z_ID);

    bool IsInterested;
    public bool CanShowInterest => IsAdoptable && AppSession.IsLoggedIn && !IsInterested; // binding for the buttons visibility
    public RelayCommand ExpressInterestCmd{get;}
    public CatProfileViewModel(Cat c)
    {
        CurrentCat = c;
        ExpressInterestCmd = new RelayCommand(ExpressAdoptionInterest);
        // Check if the user already has a relationship with this cat
        AdopterRepo ar = new AdopterRepo(Program.Configuration.GetConnectionString("Catabase"));
        if(Session.IsLoggedIn)
            IsInterested = ar.DoesInterestExist(Session.CurrentUser.PersonID, CurrentCat.Id);
    }
    void ExpressAdoptionInterest()
    {
        var adoptionInterest = new AdoptionInterest(CurrentCat.Id, AppSession.CurrentUser.PersonID);
    }

}