
using CatDataViewer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CatDataViewer.Views.ColonyMap;
using System;
namespace CatDataViewer.ViewModels;
public class MapViewModel: ViewModelBase
{
    // Repositories
    ZoneRepo zoneRepo = ZoneRepo.getInstance(Program.Configuration.GetConnectionString("Catabase"));
    CatRepository catRepository = new CatRepository(Program.Configuration.GetConnectionString("Catabase"));

    // Properties
    public Uri MapImage
    {
        get
        {
            Console.WriteLine("Map image requested");
            return new Uri("avares://CatDataViewer/Assets/CampusMap.jpg");
        }
    }
    public ObservableCollection<Zone> Zones{get;}
    // Content Control
    private object? zoneView;
    public object? ZoneView
    {
        get => zoneView;
        set
        {
            zoneView = value;
            OnPropertyChanged();
        }
    }
    public bool IsZoneSelected => zoneView != null;
    
    // Commands
    public RelayCommand<int> OpenZoneCmd{get;}

    public (int Canyon, int CCenter, int Tower, int UP, int Barret) ZoneNums{get;} = (4, 1, 5, 8, 6); // temporary solution to getting ZoneIDs, ideally need to bind each zone object to it's respective control
    // Constructor
    public MapViewModel()
    {
        Zones = new ObservableCollection<Zone>(zoneRepo.SelectAll());
        ZoneView = null;
        OpenZoneCmd = new RelayCommand<int>(OpenZone);
    }


    void OpenZone(int id)
    {
        Zone match;
        foreach(Zone z in Zones)
        {
            if(z.Id == id)
            {
                match = z;
                ZoneView = new ZoneViewer(match);
                break;
            }
        }
    }
    
}