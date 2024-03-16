using System;
using System.Net;
using System.Net.Sockets;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using JAC.Service.Core;
using JAC.Shared;
using JAC.Views;

namespace JAC;

public partial class JACClient : Window
{
    #region instancevariables
    readonly MainView _mainView;

    readonly ChatClient _chatClient;
    #endregion
    
    #region constructors
    public JACClient()
    {
        InitializeComponent();
        _chatClient = ChatClient.GetInstance();
        
        TbxDate.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
        LblRequestTime.Content = $"{DateTime.Now.ToShortTimeString()}";
    }
    
    public JACClient(MainView mainView)
    {
        _mainView = mainView;
        InitializeComponent();
        _chatClient = ChatClient.GetInstance();
        
        TbxDate.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
        LblRequestTime.Content = $"{DateTime.Now.ToShortTimeString()}";
    }
    #endregion

    #region methods
    private void TbxRequest_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxRequest.Text))
        {
            TblPlaceHolder.IsVisible = false;
        }
        else
        {
            TblPlaceHolder.IsVisible = true;
        }
    }
    
    private void TbxRequest_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnSendMessage_OnClick(sender, e);
        }
    }

    private void BtnClearMessage_OnClick(object sender, RoutedEventArgs e)
    {
        TbxRequest.Text = "";
        TbxRequest.Focus();
        TblPlaceHolder.IsVisible = true;
    }

    private void BtnSendMessage_OnClick(object sender, RoutedEventArgs e)
    {
        LblMessageMissing.IsVisible = false;
        
        if (!string.IsNullOrEmpty(TbxRequest.Text))
        {
            if (TbxRequest.Text == "exit")
            {
                LblStatus.Content = "Not Connected";
                LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 72, 111));

                Close();
            }

            SocketWriter writer = _chatClient.Writer;
            SocketReader reader = _chatClient.Reader;
                
            writer.SendText($"/broadcast {TbxRequest.Text}");
            string response = reader.ReceiveText();

            TbxResponse.Text = response;
                
            LblRequestTime.Content = $"{DateTime.Now.ToShortTimeString()}";
            LblResponseTime.Content = $"{DateTime.Now.ToShortTimeString()}";
                
            TbxRequest.Text = "";
        }
        else
        {
            LblMessageMissing.IsVisible = true;
        }
    }
    #endregion
}