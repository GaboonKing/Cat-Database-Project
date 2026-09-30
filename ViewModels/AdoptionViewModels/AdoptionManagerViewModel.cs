using System;
using CatDataViewer.ViewModels;
using CatDataViewer.Views.Adoptions;
using CommunityToolkit.Mvvm.Input;

public class AdoptionManagerViewModel : ViewModelBase
{
    private Object? adoptionView;
    public Object AdoptionView
    {
        get => adoptionView;
        set
        {
            if(value != adoptionView)
            {
                adoptionView = value;
                OnPropertyChanged();
            }
        }
    }
    public RelayCommand ReturnCmd{get;}
    public AdoptionManagerViewModel()
    {
        AdoptionView = new InterestList();
        ReturnCmd = new RelayCommand(Return);
    }
    void Return()
    {
        Session.CurrentView = new AdoptionPage();
    }


}