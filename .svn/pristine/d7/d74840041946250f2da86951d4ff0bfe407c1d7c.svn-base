using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using JAC.Service.Core;

namespace JACService;

public partial class MainWindow : Window
{
    #region instancevariables
    readonly TcpService _tcpService;

    readonly FileServiceLogger _serviceLogger;

    int _counterIpChanged;
    
    int _counterPortChanged;
    #endregion
    
    #region constructor
    public MainWindow()
    {
        InitializeComponent();
        
        Server.IsVisible = true;
        BrdOptions.IsVisible = false;
        
        _serviceLogger = new FileServiceLogger();
        _serviceLogger.LogUpdated += ServiceLogger_LogUpdated!;
        UpdateLogListBox();
        
        _tcpService = new TcpService(_serviceLogger);
        
        LblDate.Content = DateTime.Today.Date.ToString("dd.MM.yyyy");

        _counterIpChanged = 0;
        _counterPortChanged = 0;
    }
    #endregion

    #region methods
    private void BtnOptions_OnClick(object sender, RoutedEventArgs e)
    {
        BrdOptions.IsVisible = true;
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
                
                BtnOptions.IsEnabled = false;
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
                
                BtnOptions.IsEnabled = true;
            }
        }
    }
    
    private void BtnClear_OnClick(object sender, RoutedEventArgs e)
    {
        List<LogMessage> logMessages = _serviceLogger.GetLogMessages();
        logMessages.Clear();
        
        UpdateLogListBox();
    }
    
    private void TbxIp_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_counterIpChanged >= 1)
        {
            BtnBack.IsEnabled = false;
        }
        
        if (!string.IsNullOrEmpty(TbxIp.Text))
        {
            TblPlaceHolderIp.IsVisible = false;
        }
        else
        {
            TblPlaceHolderIp.IsVisible = true;
        }
        
        _counterIpChanged++;
    }
    
    private void TbxPort_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_counterPortChanged >= 1)
        {
            BtnBack.IsEnabled = false;
        }
        
        if (!string.IsNullOrEmpty(TbxPort.Text))
        {
            TblPlaceHolderPort.IsVisible = false;
        }
        else
        {
            TblPlaceHolderPort.IsVisible = true;
        }
        
        _counterPortChanged++;
    }
    
    private void BtnSave_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxPort.Text) && int.TryParse(TbxPort.Text, out int port) && port is >= 1024 and <= 65535)
        {
            if (!string.IsNullOrEmpty(TbxIp.Text) && IPAddress.TryParse(TbxIp.Text, out IPAddress? ip))
            {
                TcpService.Ip = ip.ToString();
                TcpService.Port = port;
            }
            else
            {
                TbxIp.Text = TcpService.DefaultIp;
                TcpService.Port = port;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(TbxIp.Text) && IPAddress.TryParse(TbxIp.Text, out IPAddress? ip))
            {
                TbxPort.Text = TcpService.DefaultPort.ToString();
                TcpService.Ip = ip.ToString();
            }
            else
            {
                TbxIp.Text = TcpService.DefaultIp;
                TbxPort.Text = TcpService.DefaultPort.ToString();
            }
        }
        
        BtnBack.IsEnabled = true;
    }
    
    private void BtnClose_OnClick(object sender, RoutedEventArgs e)
    {
        BrdOptions.IsVisible = false;
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