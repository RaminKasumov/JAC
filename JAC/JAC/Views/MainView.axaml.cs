using System.Drawing;
using System.Net;
using System.Net.Sockets;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using JAC.Shared;
using Color = Avalonia.Media.Color;

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
    }
    #endregion

    #region methods
    private void TbxCommand_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxCommand.Text))
        {
            TblPlaceHolderCommand.IsVisible = false;
        }
        else
        {
            TblPlaceHolderCommand.IsVisible = true;
        }
    }

    private void BtnClearCommand_OnClick(object? sender, RoutedEventArgs e)
    {
        TbxCommand.Text = "";
        TbxCommand.Focus();
        TblPlaceHolderCommand.IsVisible = true;
    }
    
    private void BtnConnect_OnClick(object? sender, RoutedEventArgs e)
    {
        if (LblStatus.Content == "Connected")
        {
            //MessageBox.Show("Socket is already connected!!!", "SOCKET IS CONNECTED", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 4711);

            _clientSocket.Connect(endPoint);
            TbxResponse.Text = "Connected to server";
            
            LblStatus.Content = "Connected";
            LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(20, 157, 77));
        }
    }

    private void BtnSendCommand_OnClick(object? sender, RoutedEventArgs e)
    {
        if (LblStatus.Content == "Connected")
        {
            if (!string.IsNullOrEmpty(TbxCommand.Text))
            {
                if (TbxCommand.Text == "exit")
                {
                    LblStatus.Content = "Not Connected";
                    LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 72, 111));
                }
                
                _writer.SendText(TbxCommand.Text);

                string response = _reader.ReceiveText();

                TbxResponse.Text = response;
                
                TbxCommand.Text = "";
            }
            else
            {
                //MessageBox.Show("Please enter a command!!!", "NO COMMAND", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            //MessageBox.Show("Client is not connected to server!!!", "CLIENT IS NOT CONNECTED", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    #endregion
}