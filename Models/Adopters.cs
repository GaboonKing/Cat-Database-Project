using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
namespace CatDataViewer.Models;


interface IPerson
{
    int PersonID{get;set;}
    string F_Name{get;set;}
    string L_Name{get;set;}
    int Age{get;set;}
}

public class Adopter : IPerson
{
    string password;
    string type;

    public Adopter()
    {
        PersonID = 0;
        F_Name = "none";
        L_Name = "none";
        Age = 0;
        Type = "other";
        Status = "none";
        Email = "none";
        Password = "none";
    }
    
    public Adopter(int ID, string f_name, string l_name, int age, string type, string status, string email, string password)
    {
        PersonID = ID;
        F_Name = f_name;
        L_Name = l_name;
        Age = age;
        Type = type;
        Status = status;
        Email = email;
        Password = password;
    }
    
    
    public int PersonID{get;set;}
    public string F_Name{get;set;}
    
        
    
    public string L_Name{get;set;}
    public int Age{get;set;}

    public string Status{get; set;}

    public string Email{get;set;}

    public string Password
    {
        get => password;
        set
        {
            password = value;
        }
    }

    public string Type
    {
        get{return type;}

        set
        {
            type = value;
        }
        
    }

}