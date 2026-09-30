using System.Collections.ObjectModel;
using CatDataViewer;
using CatDataViewer.Models;
using Microsoft.Extensions.Configuration;


public class CatBrowserViewModel
{
    //public IRelayCommand<Cat> OpenProfileCmd {get;}
    public ObservableCollection<Cat> Cats {get;} = 
        new ObservableCollection<Cat>();
    
    public CatBrowserViewModel()
    {
        var cp = new CatRepository(Program.Configuration.GetConnectionString("Catabase"));
        Cats = new ObservableCollection<Cat>(cp.GetFeral());
        //OpenProfileCmd = new RelayCommand<Cat>(OpenProfile);
        
    }
    
    /*
    private void OpenProfile(Cat cat)
    {
        var p = new ProfileView(cat);
        p.Show();
    }
    */
    
}