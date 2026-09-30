using CommunityToolkit.Mvvm.Input; // may need later
using CatDataViewer.Views.ColonyMap;
using CatDataViewer.Views.Adoptions;
using CatDataViewer.Views;

using System.ComponentModel;
using CatDataViewer;
using CatDataViewer.ViewModels;
using System.Runtime.CompilerServices;
using System;


public class MainWindowViewModel : ViewModelBase
{
    //public bool LoggedIn => Session.IsLoggedIn;
    //public bool NotLoggedIn => !Session.IsLoggedIn;
    
    public SessionManager AppSession => Session;
        
     // holds the current view to be displayed
  
    public RelayCommand ViewCatsCmd{get;} // cmds need to be a property so it can be Binded
    public RelayCommand HomeCmd{get;}
    public RelayCommand AdoptionPageCmd{get;}
    public RelayCommand MapCmd{get;}
    public string SiteName{get;set;} = "Catabase"; // Holds the site name that appears in the top left
    public MainWindowViewModel()
    {
        Session.CurrentView = new FrontPage(); // FrontPage is our starting view
        ViewCatsCmd = new RelayCommand(OpenCatBrowser);
        HomeCmd = new RelayCommand(ReturnHome);
        AdoptionPageCmd = new RelayCommand(OpenAdoptionPage);
        MapCmd = new RelayCommand(OpenMap);
        
    }

    private void ReturnHome() // takes us back to the homepage
    {
        if(Session.CurrentView is not FrontPage)
        {
            //Console.WriteLine(CurrentView?.GetType().Name);
            Session.CurrentView = new FrontPage();
            //Console.WriteLine(CurrentView?.GetType().Name);
        }
    }
    private void OpenCatBrowser()
    {
        if(Session.CurrentView is not CatBrowserWindow)
        {
            //Console.WriteLine(CurrentView?.GetType().Name);
            Session.CurrentView = new CatBrowserWindow();
            //Console.WriteLine(CurrentView?.GetType().Name);
        } 
    }
    private void OpenAdoptionPage()
    {
        if(Session.CurrentView is not AdoptionPage)
            Session.CurrentView = new AdoptionPage();
    }
    private void OpenMap()
    {
        if(Session.CurrentView is not Map)
        {
            Console.WriteLine($"Changing to {new Map().GetType()}");
            Session.CurrentView = new Map();
            Console.WriteLine($"View Changed, {Session.CurrentView?.GetType()}");
        }
    }
    
}