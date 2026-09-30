using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace CatDataViewer.Views;
public partial class FrontPage : UserControl
{
    private TranslateTransform _transform = new();
    private const double speed = 2;
    private const double LoopHeight = 750;
    
    public FrontPage()
    {
        InitializeComponent();
        DataContext = new FrontPageViewModel();

        
        ImagePanel.RenderTransform = _transform;
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(16);
        _transform.Y = -10;
        timer.Tick += (_,_) =>
        {
            _transform.Y -= speed;
            if(_transform.Y <= -LoopHeight)
            {
                _transform.Y = 0;
            }
        };

        timer.Start();
        
    }
}