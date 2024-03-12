using System;
using System.Net;
using System.Net.Sockets;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using JAC.Shared;

namespace JAC.Views;

public partial class MainView : UserControl
{
    #region instancevariables
    readonly int _bufferSize = 1024;

    readonly Socket _clientSocket;
        
    readonly SocketReader _reader;
        
    readonly SocketWriter _writer;
    #endregion
    
    #region constructor
    public MainView()
    {
        InitializeComponent();
        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _reader = new SocketReader(_clientSocket, _bufferSize);
        _writer = new SocketWriter(_clientSocket);
        
        TbxDate.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
        LblNicknameTime.Content = $"{DateTime.Now.ToShortTimeString()}";
    }
    #endregion

    #region methods
    private void TbxNickname_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxNickname.Text))
        {
            TblPlaceHolderCommand.IsVisible = false;
        }
        else
        {
            TblPlaceHolderCommand.IsVisible = true;
        }
    }
    
    private void TbxNickname_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnLogin_OnClick(sender, e);
        }
    }

    private void BtnClearCommand_OnClick(object sender, RoutedEventArgs e)
    {
        TbxNickname.Text = "";
        TbxNickname.Focus();
        TblPlaceHolderCommand.IsVisible = true;
    }
    
    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
    #endregion
}