using System;
using CatDataViewer;
using CatDataViewer.Models;
using CatDataViewer.Views;
using Microsoft.Extensions.Configuration;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using CatDataViewer.ViewModels;

public class FrontPageViewModel : ViewModelBase
{
    private CatRepository cat_repo = new CatRepository(Program.Configuration.GetConnectionString("Catabase")); // database link
    public Cat CatOfTheDay{get;set;}
    // Properties of COD that we need to display
    public string CatName => CatOfTheDay.Name;
    public string CatBio => CatOfTheDay.Bio;
    public Bitmap ImagePath => CatOfTheDay.RawImagePath;
    ZoneRepo z = ZoneRepo.getInstance(Program.Configuration.GetConnectionString("Catabase"));
    public string Location => ZoneRepo.GetName(CatOfTheDay.z_ID);
    public RelayCommand OpenCatWindow{get;}

    //public ObservableCollection<Cat> CatImages {get;} = new ObservableCollection<Cat>();
    
    public FrontPageViewModel()
    {
        SelectCOD();
        OpenCatWindow = new RelayCommand(OpenCatViewer);
        /*CatImages = new ObservableCollection<Cat>(cat_repo.SelectAll());
        ObservableCollection<Cat> dupe = new ObservableCollection<Cat>(cat_repo.SelectAll());
        foreach(Cat c in dupe)
            CatImages.Add(c); // doubling the collection size for a smoother transition
        foreach(Cat c in CatImages)
            Console.WriteLine(c.RawImagePath);
        Console.WriteLine(CatImages.Count());*/
       
    }
    private void SelectCOD() // COD = Cat Of the Day
    {
        
        var cats = cat_repo.GetFeral(); // get a list of the current feral cats
        // Set up seed based off the current date
        DateTime today = DateTime.Today;
        int seed = today.Year + today.Month + today.Day;
        Random rnd = new Random(seed);

        //Retrieve the cat
        int index = rnd.Next(0, cats.Count); // selects a random index within the size range of cats
        CatOfTheDay = cats[index];
        
    }
    
    private void OpenCatViewer()
    {
        Session.CurrentView = new CatBrowserWindow();
    }
}