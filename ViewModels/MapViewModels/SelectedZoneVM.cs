using CatDataViewer.ViewModels;
using CatDataViewer.Models;
using Avalonia.Media.Imaging;
using System.Runtime.InteropServices;
using CatDataViewer;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System;
namespace CatDataViewer.ViewModels;
public class SelectedZoneVM : ViewModelBase
{
    public string ZoneName{get;}
    public Bitmap RawImagePath{get;}
    public ObservableCollection<Cat> ZoneCats{get;}
    CatRepository catRepository = new CatRepository(Program.Configuration.GetConnectionString("Catabase"));
    //public readonly Action _action; // holds the action for setting the view to

    public SelectedZoneVM(Zone zone)
    {
        ZoneName = zone.Name;
        RawImagePath = zone.RawImagePath;
        ZoneCats = new ObservableCollection<Cat>(catRepository.GetCatsByZone(zone.Id));
        //_action = exitAction;
    }
    void CloseView()
    {
        //_action.Invoke();
    }
    
}