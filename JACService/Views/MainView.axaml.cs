using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using JACService.Core;

namespace JACService.Views;

public partial class MainView : UserControl
{
    #region instancevariables
    readonly TcpService _tcpService;

    readonly FileServiceLogger _serviceLogger;
    #endregion
    
    #region constructor
    public MainView()
    {
        InitializeComponent();
        
        Server.IsVisible = true;
        
        _serviceLogger = new FileServiceLogger();
        _serviceLogger.LogUpdated += ServiceLogger_LogUpdated!;
        UpdateLogListBox();
        
        _tcpService = new TcpService(_serviceLogger);
        
        LblDate.Content = DateTime.Today.Date.ToString("dd.MM.yyyy");
    }
    #endregion

    #region methods
    private async void BtnSettings_OnClick(object sender, RoutedEventArgs e)
    {
        var serverOptions = new ServerOptions();
        var cdlServerOptions = serverOptions.FindControl<ContentDialog>("CdlServerOptions");

        await cdlServerOptions!.ShowAsync();
    }
    
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
            _tcpService.Start(TcpService.Ip, TcpService.Port);

            if (_tcpService.Online)
            {
                LblStatus.Content = "Online";
                LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(20, 157, 77));
            }
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
            
            if (!_tcpService.Online)
            {
                LblStatus.Content = "Offline";
                LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 72, 111));
            }
        }
    }
    
    private void BtnClear_OnClick(object sender, RoutedEventArgs e)
    {
        List<LogMessage> logMessages = _serviceLogger.GetLogMessages();
        logMessages.Clear();
        
        UpdateLogListBox();
    }
    
    private void ServiceLogger_LogUpdated(object sender, EventArgs e)
    {
        UpdateLogListBox();
    }
        
    private async void UpdateLogListBox()
    {
        List<LogMessage> messages = _serviceLogger.GetLogMessages().ToList();

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            LbxInformation.Items.Clear();
            foreach (LogMessage message in messages)
            {
                ListBoxItem item = new ListBoxItem();
                System.Drawing.Color winFormsColor = message.Color;
                Color wpfColor = Color.FromArgb(winFormsColor.A, winFormsColor.R, winFormsColor.G, winFormsColor.B);
                SolidColorBrush brush = new SolidColorBrush(wpfColor);
                item.Content = message.ToString();
                item.Foreground = brush;
                LbxInformation.Items.Add(item);
            }
        });
    }
    #endregion
}