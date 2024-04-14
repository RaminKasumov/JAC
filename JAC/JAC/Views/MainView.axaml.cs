using System;
using System.Net;
using System.Net.Sockets;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using JAC.Service.Core;
using JAC.Shared;

namespace JAC.Views;

public partial class MainView : UserControl
{
    #region instancevariables
    readonly Socket _clientSocket;
    
    readonly SocketWriter _writer;
    
    readonly SocketReader _reader;
    #endregion

    #region constructor
    public MainView()
    {
        InitializeComponent();
        
        Client.IsVisible = false;
        Login.IsVisible = true;

        ChatClient chatClient = ChatClient.GetInstance();
        _clientSocket = chatClient.ClientSocket;
        _reader = chatClient.Reader;
        _writer = chatClient.Writer;
        
        TbxLoginDate.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
        TbxRequestDate.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
        
        LblNicknameTime.Content = $"{DateTime.Now.ToShortTimeString()}";
        LblRequestTime.Content = $"{DateTime.Now.ToShortTimeString()}";
    }
    #endregion

    #region methods
    private void TbxNickname_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxNickname.Text))
        {
            TblPlaceHolder.IsVisible = false;
        }
        else
        {
            TblPlaceHolder.IsVisible = true;
        }
    }
    
    private void TbxRequest_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxRequest.Text))
        {
            TblPlaceHolderMessage.IsVisible = false;
        }
        else
        {
            TblPlaceHolderMessage.IsVisible = true;
        }
    }
    
    private void TbxNickname_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnLogin_OnClick(sender, e);
        }
    }
    
    private void TbxRequest_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnSendMessage_OnClick(sender, e);
        }
    }

    private void BtnClear_OnClick(object sender, RoutedEventArgs e)
    {
        TbxNickname.Text = "";
        TbxNickname.Focus();
        TblPlaceHolder.IsVisible = true;
    }
    
    private void BtnClearMessage_OnClick(object sender, RoutedEventArgs e)
    {
        TbxRequest.Text = "";
        TbxRequest.Focus();
        TblPlaceHolderMessage.IsVisible = true;
    }

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        LblNicknameMissing.IsVisible = false;
        LblLoginError.IsVisible = false;

        if (!string.IsNullOrEmpty(TbxNickname.Text))
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 4711);

            if (!_clientSocket.Connected)
            {
                _clientSocket.Connect(endPoint);
            }

            _writer.SendText(TbxNickname.Text);
            string receivedText = _reader.ReceiveText();

            if (receivedText == "User logged in.")
            {
                Client.IsVisible = true;
                Login.IsVisible = false;
            }
            else
            {
                LblLoginError.IsVisible = true;
            }
            
            TbxNickname.Text = "";
        }
        else
        {
            LblNicknameMissing.IsVisible = true;
        }
    }

    private void BtnSendMessage_OnClick(object sender, RoutedEventArgs e)
    {
        LblSocketNotConnected.IsVisible = false;
        LblMessageMissing.IsVisible = false;

        if (LblStatus.Content != null && (string)LblStatus.Content == "Connected")
        {
            if (!string.IsNullOrEmpty(TbxRequest.Text))
            {
                if (TbxRequest.Text == "/broadcast exit")
                {
                    LblStatus.Content = "Not Connected";
                    LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 72, 111));
                }
            
                _writer.SendText(TbxRequest.Text);
                string response = _reader.ReceiveText();

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
        else
        {
            LblSocketNotConnected.IsVisible = true;
        }
    }
    #endregion
}