
using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MySqlConnector;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
namespace CatDataViewer.Models;

public class Cat
{
    
    public Cat(int id, string name, char g, int age, string status, bool fix, string temperament, string b, int z, string img_p)
    {
        Id = id;
        Name = name;
        Gender = g;
        Age = age;
        Status = status;
        IsFixed = fix;
        Temperament = temperament;
        Bio = b;
        z_ID = z;
        img = img_p.ToString();

        
    }
    string img;
    string bio;

    // Properties
    public int Id{get;set;}
    public int z_ID;
    public string Name{get;set;}
    public char Gender{get;set;}
    public int Age{get;set;}
    public string Status{get;set;}
    public bool IsFixed{get;set;}
    public string Temperament{get;set;} 
    public string Bio
    {
        get
        {
            return bio;
        }
        set
        {
           if(value == "")
                bio = "This cat doesn't have bio yet!";
        }
        
    }
    
    public Bitmap RawImagePath
    {
        get
        {
            string normalized = img.Replace('\\', '/');
            string output = $"avares://CatDataViewer/{normalized}";
            try
            {
                var uri = new Uri(output);
                using var stream = AssetLoader.Open(uri);
                return new Bitmap(stream);
            }
            catch(Exception e)
            {
                Console.WriteLine($"{Name}'s image could not be loaded: {e.Message}");
                using var stream = AssetLoader.Open(new Uri(DefaultImage));
                return new Bitmap(stream);
            }
        }
        set
        {
           img = value.ToString(); 
        }
    }

    string DefaultImage = "avares://CatDataViewer/Assets/CatPhotos/test.jpg";
   
   
}



