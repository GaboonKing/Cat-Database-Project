using Avalonia;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CatDataViewer;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    
    public static IConfiguration Configuration{get;private set;}
    [STAThread]
    public static void Main(string[] args)
    {
        
        //Console.WriteLine("TEST");
        //File.WriteAllText("main.txt", DateTime.Now.ToString());
        // Create a configuration so we can access the MySQL connection string 
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

    }
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
