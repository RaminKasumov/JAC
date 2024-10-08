﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;
using JACService.Core;
using JACService.Shared;

namespace JAC.Views;

public partial class MainView : UserControl
{
    #region instancevariables
    Socket _clientSocket = null!;
    
    SocketWriter _writer = null!;

    IUser _chatUser = new ChatUser();

    readonly ChatMessageStorage _chatMessageStorage = ChatMessageStorage.GetInstance();

    readonly ChatDirectory _directory = ChatDirectory.GetInstance();
    
    MessageReceiver _messageReceiver = null!;
    
    string _maskedPassword = "";
    
    bool _isEditingPassword;

    Border _brdChannel = null!;
    #endregion

    #region constructor
    public MainView()
    {
        InitializeComponent();
        
        Login.IsVisible = true;
        ChatApp.IsVisible = false;
        ChannelName.IsVisible = false;
        Chat.IsVisible = false;
        JustAnotherChat.IsVisible = true;
        
        LblLoginDate.Content = DateTime.Today.Date.ToString("dd.MM.yyyy");
        LblRequestDate.Content = DateTime.Today.Date.ToString("dd.MM.yyyy");
    }
    #endregion

    #region methods
    private void TbxNickname_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        TblPlaceHolderNickname.IsVisible = string.IsNullOrEmpty(TbxNickname.Text);
    }
    
    private void TbxPassword_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        TblPlaceHolderPassword.IsVisible = string.IsNullOrEmpty(TbxPassword.Text);

        if (_isEditingPassword)
        {
            if (!string.IsNullOrEmpty(TbxPassword.Text))
            {
                _maskedPassword = TbxPassword.Text;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(TbxPassword.Text) && TbxPassword.Text.Length > _maskedPassword.Length)
            {
                _maskedPassword += TbxPassword.Text[^1];
                TbxPassword.Text = new string('*', _maskedPassword.Length);
            }
            else
            {
                if (!string.IsNullOrEmpty(TbxPassword.Text))
                {
                    _maskedPassword = _maskedPassword.Substring(0, TbxPassword.Text.Length);
                }
            }
        }
    }
    
    private void BtnShowPassword_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TbxPassword.Text)) return;
        TbxPassword.Text = _maskedPassword;
        _isEditingPassword = true;
        
        BtnShowPassword.IsVisible = false;
        BtnHidePassword.IsVisible = true;
    }

    private void BtnHidePassword_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TbxPassword.Text)) return;
        TbxPassword.Text = new string('*', _maskedPassword.Length);
        _isEditingPassword = false;
        
        BtnShowPassword.IsVisible = true;
        BtnHidePassword.IsVisible = false;
    }
    
    private void TbxNickname_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnLogin_OnClick(sender, e);
        }
    }
    
    private void TbxPassword_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnLogin_OnClick(sender, e);
        }
    }
    
    private void TbxRequest_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxRequest.Text))
        {
            TblPlaceHolderMessage.IsVisible = false;
            BtnSendMessage.IsVisible = true;
        }
        else
        {
            TblPlaceHolderMessage.IsVisible = true;
            BtnSendMessage.IsVisible = false;
        }
    }
    
    private void TbxRequest_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnSendMessage_OnClick(sender, e);
        }
    }
    
    private void BtnExit_OnClick(object? sender, RoutedEventArgs e)
    {
        _writer.SendText($"/exit {LblNickname.Content}");
        _messageReceiver.Stop();
        
        Environment.Exit(0);
    }

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        LblInputMissing.IsVisible = false;
        LblLoginError.IsVisible = false;
        LblServerOffline.IsVisible = false;

        if (!string.IsNullOrEmpty(TbxNickname.Text) && !string.IsNullOrEmpty(_maskedPassword))
        {
            ChatClient chatClient = ChatClient.GetInstance();
            chatClient.Reinitialize();
            
            _clientSocket = chatClient.ClientSocket;
            _writer = chatClient.Writer;
        
            _messageReceiver = new MessageReceiver(_clientSocket);
            _messageReceiver.MessageReceived += OnMessageReceived;
            
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(TcpService.Ip), TcpService.Port);
            
                if (!_clientSocket.Connected)
                {
                    _clientSocket.Connect(endPoint);
                    _messageReceiver.Start();
                }
            
                _writer.SendText($"/login {TbxNickname.Text.Trim()} {_maskedPassword}");
            }
            catch (Exception)
            {
                LblServerOffline.IsVisible = true;
            }
        }
        else
        {
            LblInputMissing.IsVisible = true;
        }
    }
    
    private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
    {
        _writer.SendText($"/logout {LblNickname.Content}");
    }
    
    private void BtnSettings_OnClick(object sender, RoutedEventArgs e)
    {
        TbxName.Text = _chatUser.Nickname;
        TbxPasscode.Text = new string('*', _chatUser.Password.Length);
        
        if (BrdAddChannel.IsVisible)
        {
            BrdAddChannel.IsVisible = false;
        }
        
        BrdSettings.IsVisible = true;
    }
    
    private void BtnCloseSettings_OnClick(object sender, RoutedEventArgs e)
    {
        if (BtnHidePasscode.IsVisible)
        {
            BtnHidePasscode.IsVisible = false;
            BtnShowPasscode.IsVisible = true;
        }
        
        BrdSettings.IsVisible = false;
    }

    private void BtnAddChannel_OnClick(object sender, RoutedEventArgs e)
    {
        LblChannelInputError.IsVisible = false;
        LblChannelExistsError.IsVisible = false;
        LblUserError.IsVisible = false;
        
        TbxNewChannelName.Text = "";
        CbxChannelType.SelectedIndex = 0;
        
        if (BrdSettings.IsVisible)
        {
            BrdSettings.IsVisible = false;
        }
        
        BrdAddChannel.IsVisible = true;
    }
    
    private void BtnCloseChannel_OnClick(object? sender, RoutedEventArgs e)
    {
        BrdAddChannel.IsVisible = false;
    }
    
    private void TbxNewChannelName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        TblPlaceHolderNewChannel.IsVisible = string.IsNullOrEmpty(TbxNewChannelName.Text);
    }
    
    private void TbxNewChannelName_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            BtnAddNewChannel_OnClick(sender, e);
        }
    }
    
    private void BtnAddNewChannel_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TbxNewChannelName.Text))
        {
            string channelType = CbxChannelType.SelectedIndex == 0 ? "public" : "private";
            string channelName = TbxNewChannelName.Text.Trim();
            
            if (CbxChannelType.SelectedIndex == 0)
            {
                _writer.SendText($"/create new {channelType} Channel {channelName}");
            }
            else
            {
                _writer.SendText($"/create new {channelType} Channel {channelName}");
            }
        }
        else
        {
            LblChannelInputError.IsVisible = true;
        }
    }
    
    private void BtnRemoveChannel_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: IChannel channel })
        {
            _writer.SendText($"/delete Channel {channel.Name}");
        }
    }
    
    private void BtnShowPasscode_OnClick(object sender, RoutedEventArgs e)
    {
        TbxPasscode.Text = _maskedPassword;
        
        BtnShowPasscode.IsVisible = false;
        BtnHidePasscode.IsVisible = true;
    }

    private void BtnHidePasscode_OnClick(object sender, RoutedEventArgs e)
    {
        TbxPasscode.Text = new string('*', _maskedPassword.Length);
        
        BtnShowPasscode.IsVisible = true;
        BtnHidePasscode.IsVisible = false;
    }

    private void BtnChannel_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: IChannel channel })
        {
            _writer.SendText($"/join Channel {channel.Name}");

            if (_brdChannel is { IsVisible: true })
            {
                _brdChannel.IsVisible = false;
            }
        }
    }
    
    private void Channels_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(null).Properties.PointerUpdateKind == PointerUpdateKind.RightButtonPressed)
        {
            if (sender is ItemsControl control)
            {
                var position = e.GetPosition(control);
                var container = control.GetVisualDescendants().OfType<ContentPresenter>().FirstOrDefault(cp => cp.Bounds.Contains(position));

                if (container is { DataContext: IChannel })
                {
                    _brdChannel = container.GetVisualDescendants().OfType<Border>().FirstOrDefault(b => b.Name == "BrdChannel")!;
                    if (_brdChannel != null)
                    {
                        _brdChannel.IsVisible = true;
                    }
                }
            }
        }
    }

    private void BtnSendMessage_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TbxRequest.Text)) return;

        if (_directory.FindChannel(_chatUser.CurrentChannel.Name) != null)
        {
            _writer.SendText(!_chatUser.CurrentChannel.IsPrivate
                ? $"/broadcast {_chatUser.CurrentChannel.Name} |{TbxRequest.Text.Trim()}"
                : $"/whisper {_chatUser.CurrentChannel.Name} |{TbxRequest.Text.Trim()}");
        }

        TbxRequest.Text = "";
    }

    private void HandleMessage(string message)
    {
        if (message.StartsWith("Login-success"))
        {
            string[] splitter = message.Split(' ');
            string loggedUser = message.Substring(splitter[0].Length + 1);
            _chatUser = ChatUser.FromString(loggedUser);
            
            Channels.Items.Clear();
            
            if (_directory.FindUser(_chatUser.Nickname) == null)
            {
                _directory.AddUser(_chatUser);
                
                foreach (IChannel channel in _directory.GetChannels())
                {
                    if (!channel.IsPrivate)
                    {
                        channel.AddUser(_chatUser);
                        
                        Channels.Items.Add(channel);
                    }
                }
            }
            else
            {
                IUser user = _directory.FindUser(_chatUser.Nickname);
                
                foreach (IChannel channel in _directory.GetChannels())
                {
                    if (channel.Users.Contains(user))
                    {
                        Channels.Items.Add(channel);
                    }
                }
            }

            ChannelsScrollViewer.ScrollToEnd();
            
            Login.IsVisible = false;
            ChatApp.IsVisible = true;
            JustAnotherChat.IsVisible = true;
            
            LblNickname.Content = _chatUser.Nickname;
            
            TbxNickname.Text = "";
            TbxPassword.Text = "";
        }
        else if (message.StartsWith("Logout-success"))
        {
            _maskedPassword = "";

            Channels.Items.Clear();
            Messages.Items.Clear();

            Login.IsVisible = true;
            ChatApp.IsVisible = false;
            ChannelName.IsVisible = false;
            JustAnotherChat.IsVisible = false;
            Chat.IsVisible = false;
            BrdSettings.IsVisible = false;
            BrdAddChannel.IsVisible = false;
        }
        else if (message.StartsWith("MessageReceived"))
        {
            string[] splitter = message.Split(' ');
            string messageString = message.Substring(splitter[0].Length).Trim();
            
            IChatMessage chatMessage = ChatMessage.FromString(messageString);
            _chatMessageStorage.AddMessage(_chatUser.CurrentChannel.Name, chatMessage);
            AddMessage(chatMessage);
        }
        else if (message.StartsWith("Channel"))
        {
            string[] splitter = message.Split(' ');
            
            if (message.Contains("joined"))
            {
                string channelName = splitter[1].Trim();
                IChannel channel = _directory.FindChannel(channelName);
                
                _chatUser.CurrentChannel = channel;
                LblChannel.Content = _chatUser.CurrentChannel.Name;
            
                _chatUser.CurrentChannel.LoadMessages();
            
                Messages.Items.Clear();
                foreach (IChatMessage chatMessage in _chatUser.CurrentChannel.Messages)
                {
                    Messages.Items.Add(chatMessage);
                }

                MessagesScrollViewer.ScrollToEnd();

                ChannelName.IsVisible = true;
                Chat.IsVisible = true;
                JustAnotherChat.IsVisible = false;
            }
            else if (message.Contains("created"))
            {
                string newChannelName = splitter[1].Trim();
                IChannel newChannel = new Channel(newChannelName);
                
                if (CbxChannelType.SelectedIndex == 1)
                {
                    newChannel.IsPrivate = true;

                    string[] splitterName = newChannelName.Split('/');
                    IUser user = _directory.FindUser(splitterName[1]);
                    newChannel.AddUser(user);
                }
                
                newChannel.AddUser(_chatUser);
                _directory.AddChannel(newChannel);
                
                Channels.Items.Clear();
                foreach (IChannel channel in _directory.GetChannels())
                {
                    if (channel.Users.Contains(_chatUser))
                    {
                        Channels.Items.Add(channel);
                    }
                }
                
                ChannelsScrollViewer.ScrollToEnd();
                
                LblChannelInputError.IsVisible = false;
                LblChannelExistsError.IsVisible = false;
                LblUserError.IsVisible = false;
                
                BrdAddChannel.IsVisible = false;
                
                _writer.SendText($"/join Channel {newChannelName}");
            }
            else if (message.Contains("deleted"))
            {
                string channelName = splitter[1].Trim();
                IChannel removedChannel = _directory.FindChannel(channelName);
                
                if (_chatUser.CurrentChannel == removedChannel)
                {
                    _writer.SendText($"/join Channel Anonymous");
                }
                
                _directory.RemoveChannel(removedChannel);
                
                Channels.Items.Clear();
                foreach (IChannel channel in _directory.GetChannels())
                {
                    Channels.Items.Add(channel);
                }
                
                ChannelsScrollViewer.ScrollToEnd();

                BrdAddChannel.IsVisible = false;
            }
            else if (message.Contains("exists"))
            {
                LblChannelExistsError.IsVisible = true;
            }
        }
        else if (message.StartsWith("Error"))
        {
            if (message.Contains("password"))
            {
                LblLoginError.IsVisible = true;
            
                _maskedPassword = "";
                TbxPassword.Text = "";
            }
            else if (message.Contains("exist"))
            {
                LblUserError.IsVisible = true;
                
                TbxNewChannelName.Text = "";
            }
        }
    }
    
    private void AddMessage(IChatMessage message)
    {
        Messages.Items.Add(message);
        
        MessagesScrollViewer.ScrollToEnd();
    }
    
    private void OnMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        Dispatcher.UIThread.InvokeAsync(() => HandleMessage(e.Message));
    }
    #endregion
}