using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using Android.Net.Wifi.P2p;
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
        
        Login.IsVisible = true;
        ChatApp.IsVisible = false;
        ChannelName.IsVisible = false;
        Chat.IsVisible = false;

        ChatClient chatClient = ChatClient.GetInstance();
        _clientSocket = chatClient.ClientSocket;
        _reader = chatClient.Reader;
        _writer = chatClient.Writer;
        
        LblLoginDate.Content = DateTime.Today.Date.ToString("dd.MM.yyyy");
        LblRequestDate.Content = DateTime.Today.Date.ToString("dd.MM.yyyy");
        
        LblNicknameTime.Content = $"{DateTime.Now.ToShortTimeString()}";
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

    private void BtnClearMessage_OnClick(object sender, RoutedEventArgs e)
    {
        TbxNickname.Text = "";
        TbxNickname.Focus();
        TblPlaceHolder.IsVisible = true;
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

            _writer.SendText($"/login {TbxNickname.Text}");
            string receivedText = _reader.ReceiveText();

            if (receivedText == "User logged in.")
            {
                Login.IsVisible = false;
                ChatApp.IsVisible = true;
                LblNickname.Content = TbxNickname.Text;
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

    private void BtnAnonymousChat_OnClick(object sender, RoutedEventArgs e)
    {
        ChannelName.IsVisible = true;
        Chat.IsVisible = true;
        LblResponseTime.Content = $"{DateTime.Now.ToShortTimeString()}";
    }

    private void BtnSendMessage_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxRequest.Text))
        {
            _writer.SendText(TbxRequest.Text);
            string response = _reader.ReceiveText();
                
            TbxRequest.Text = "";
        }
    }
    #endregion
}