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
    private void TbxRequest_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxRequest.Text))
        {
            TblPlaceHolderCommand.IsVisible = false;
        }
        else
        {
            TblPlaceHolderCommand.IsVisible = true;
        }
    }

    private void BtnClearCommand_OnClick(object sender, RoutedEventArgs e)
    {
        TbxRequest.Text = "";
        TbxRequest.Focus();
        TblPlaceHolderCommand.IsVisible = true;
    }
    
    private void BtnConnect_OnClick(object sender, RoutedEventArgs e)
    {
        LblSocketConnected.IsVisible = false;
        LblMessageMissing.IsVisible = false;
        LblSocketNotConnected.IsVisible = false;
        
        if (LblStatus.Content != null && (string)LblStatus.Content == "Connected")
        {
            LblSocketConnected.IsVisible = true;
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

    private void BtnSendCommand_OnClick(object sender, RoutedEventArgs e)
    {
        LblSocketConnected.IsVisible = false;
        LblMessageMissing.IsVisible = false;
        LblSocketNotConnected.IsVisible = false;
        
        if (LblStatus.Content != null && (string)LblStatus.Content == "Connected")
        {
            if (!string.IsNullOrEmpty(TbxRequest.Text))
            {
                if (TbxRequest.Text == "exit")
                {
                    LblStatus.Content = "Not Connected";
                    LblStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 72, 111));
                }
                
                _writer.SendText(TbxRequest.Text);

                string response = _reader.ReceiveText();

                TbxResponse.Text = response;
                
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