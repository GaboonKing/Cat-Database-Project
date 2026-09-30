using Avalonia.Platform;
using Avalonia.Media.Imaging;
using System;

namespace CatDataViewer.Models;
public class Zone
{
    public int Id{get;set;}
    public string Name{get;set;}
    string img;

    public Zone(int id, string name, string imagePath)
    {
        Id = id;
        Name = name;
        img = imagePath;
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

    string DefaultImage = "avares://CatDataViewer/Assets/CatPhotos/test.jpg"; // for debugging
}