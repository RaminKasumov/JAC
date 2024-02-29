using System.IO;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using JAC.Service.Core;

namespace JACService;

public partial class MainWindow : Window
{
    #region instancevariable
    readonly int _backLog = 20;

    readonly TcpService _tcpService;
        
    readonly FileServiceLogger _serviceLogger;
    #endregion
    
    #region constructor
    public MainWindow()
    {
        InitializeComponent();
        _serviceLogger = new FileServiceLogger();
            
        _tcpService = new TcpService(4711, _serviceLogger);
    }
    #endregion

    #region methods
    private void BtnStart_OnClick(object sender, RoutedEventArgs e)
    {
        LblServerOnline.IsVisible = false;
        LblServerOffline.IsVisible = false;
        
        if (_tcpService.Online)
        {
            LblServerOnline.IsVisible = true;
        }
        else
        {
            _tcpService.Start(_backLog);
            
            LblStatus.Content = "Online";
            LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(20, 157, 77));
        }
    }

    private void BtnStop_OnClick(object sender, RoutedEventArgs e)
    {
        LblServerOnline.IsVisible = false;
        LblServerOffline.IsVisible = false;
        
        if (!_tcpService.Online)
        {
            LblServerOffline.IsVisible = true;
        }
        else
        {
            _tcpService.Stop();
            
            LblStatus.Content = "Offline";
            LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 72, 111));
        }
    }
    #endregion
}