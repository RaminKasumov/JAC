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
    readonly JACClient _client;

    readonly ChatClient _chatClient;
    #endregion

    #region constructor
    public MainView()
    {
        InitializeComponent();
        _client = new JACClient(this);
        _chatClient = ChatClient.GetInstance();
        
        TbxDate.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
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
    
    private void TbxNickname_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnLogin_OnClick(sender, e);
        }
    }

    private void BtnClear_OnClick(object sender, RoutedEventArgs e)
    {
        TbxNickname.Text = "";
        TbxNickname.Focus();
        TblPlaceHolder.IsVisible = true;
    }

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        LblLoginError.IsVisible = false;

        IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 4711);

        Socket socket = _chatClient.ClientSocket;

        SocketWriter writer = _chatClient.Writer;
        SocketReader reader = _chatClient.Reader;

        socket.Connect(endPoint);

        writer.SendText($"/login {TbxNickname.Text}");
        string receivedText = reader.ReceiveText();

        if (receivedText == "User logged in.")
        {
            _client.Show();
            TbxNickname.Text = "";
        }
        else
        {
            LblLoginError.IsVisible = true;
        }
    }
    #endregion
}