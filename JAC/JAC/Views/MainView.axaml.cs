﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;
using FluentAvalonia.UI.Controls;
using JACService.Core;
using JACService.Core.Contracts;
using JACService.Shared;

namespace JAC.Views;

public partial class MainView : UserControl
{
    #region instancevariables
    readonly ChatUserStorage _chatUserStorage = ChatUserStorage.GetInstance();
    
    readonly ChatChannelStorage _chatChannelStorage = ChatChannelStorage.GetInstance();
    
    readonly ChatMessageStorage _chatMessageStorage = ChatMessageStorage.GetInstance();

    readonly ChatDirectory _directory = ChatDirectory.GetInstance();

    MessageReceiver _messageReceiver = null!;
    
    bool _isEditingPassword;

    Border _brdChannel = null!;

    NewChannel _newChannel = null!;

    ContentDialog _cdlAccountOptions = null!;

    ContentDialog _cdlNewChannel = null!;
    #endregion
    
    #region properties
    Socket ClientSocket { get; set; } = null!;
    
    internal static SocketWriter Writer { get; private set; } = null!;

    internal static ChatUser User { get; private set; } = new();

    internal static string MaskedPassword { get; private set; } = "";
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
                MaskedPassword = TbxPassword.Text;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(TbxPassword.Text) && TbxPassword.Text.Length > MaskedPassword.Length)
            {
                MaskedPassword += TbxPassword.Text[^1];
                TbxPassword.Text = new string('*', MaskedPassword.Length);
            }
            else
            {
                if (!string.IsNullOrEmpty(TbxPassword.Text))
                {
                    MaskedPassword = MaskedPassword.Substring(0, TbxPassword.Text.Length);
                }
            }
        }
    }
    
    private void BtnShowPassword_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TbxPassword.Text)) return;
        TbxPassword.Text = MaskedPassword;
        _isEditingPassword = true;
        
        BtnShowPassword.IsVisible = false;
        BtnHidePassword.IsVisible = true;
    }

    private void BtnHidePassword_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TbxPassword.Text)) return;
        TbxPassword.Text = new string('*', MaskedPassword.Length);
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

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        LblInputMissing.IsVisible = false;
        LblLoginError.IsVisible = false;
        LblServerOffline.IsVisible = false;

        if (!string.IsNullOrEmpty(TbxNickname.Text) && !string.IsNullOrEmpty(MaskedPassword))
        {
            ChatClient chatClient = ChatClient.GetInstance();
            chatClient.Reinitialize();
            
            ClientSocket = chatClient.ClientSocket;
            Writer = chatClient.Writer;
        
            _messageReceiver = new MessageReceiver(ClientSocket);
            _messageReceiver.MessageReceived += OnMessageReceived;
            
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(TcpService.Ip), TcpService.Port);
            
                if (!ClientSocket.Connected)
                {
                    ClientSocket.Connect(endPoint);
                    _messageReceiver.Start();
                }
            
                Writer.SendText($"/login {TbxNickname.Text.Trim()} {MaskedPassword}");
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
    
    private void BtnExit_OnClick(object? sender, RoutedEventArgs e)
    {
        Writer.SendText($"/exit {User.Nickname}");
        _messageReceiver.Stop();
        
        Environment.Exit(0);
    }

    private async void BtnAccount_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var accountOptions = new AccountOptions();
            _cdlAccountOptions = accountOptions.FindControl<ContentDialog>("CdlAccountOptions")!;

            await _cdlAccountOptions.ShowAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }

    private async void BtnAddChannel_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _newChannel = new NewChannel();
            _cdlNewChannel = _newChannel.FindControl<ContentDialog>("CdlNewChannel")!;

            await _cdlNewChannel.ShowAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
    
    private void BtnRemoveChannel_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: IChannel channel })
        {
            Writer.SendText($"/delete Channel {channel.Name}");
        }
    }

    private void BtnChannel_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: IChannel channel })
        {
            Writer.SendText($"/join Channel {channel.Name}");

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

        if (_chatChannelStorage.FindChannel(User.CurrentChannel.Name) != null)
        {
            IChannel channel = _chatChannelStorage.FindChannel(User.CurrentChannel.Name);
            Writer.SendText(!channel.IsPrivate
                ? $"/broadcast {channel.Name} |{TbxRequest.Text.Trim()}"
                : $"/whisper {channel.Name} |{TbxRequest.Text.Trim()}");
        }

        TbxRequest.Text = "";
    }

    private void HandleMessage(string message)
    {
        if (message.StartsWith("Login-success"))
        {
            string[] splitter = message.Split(' ');
            string loggedUser = message.Substring(splitter[0].Length + 1);
            User = ChatUser.FromString(loggedUser);
            
            Channels.Items.Clear();

            if (_chatUserStorage.FindUser(User.Nickname) != null)
            {
                User = (ChatUser)_chatUserStorage.FindUser(User.Nickname);
                
                foreach (IChannel channel in _directory.Channels)
                {
                    if (channel.Users.Contains(User))
                    {
                        Channels.Items.Add(channel);
                    }
                }
            }
            else
            {
                _chatUserStorage.AddUser(User);
                
                foreach (IChannel channel in _directory.Channels)
                {
                    if (!channel.IsPrivate)
                    {
                        channel.AddUser(User);
                        
                        Channels.Items.Add(channel);
                    }
                }
            }

            ChannelsScrollViewer.ScrollToEnd();
            
            Login.IsVisible = false;
            ChatApp.IsVisible = true;
            JustAnotherChat.IsVisible = true;
            
            LblNickname.Content = User.Nickname;
            
            TbxNickname.Text = "";
            TbxPassword.Text = "";
        }
        else if (message.StartsWith("Logout-success"))
        {
            MaskedPassword = "";

            Channels.Items.Clear();
            Messages.Items.Clear();

            Login.IsVisible = true;
            ChatApp.IsVisible = false;
            ChannelName.IsVisible = false;
            JustAnotherChat.IsVisible = false;
            Chat.IsVisible = false;
            _cdlAccountOptions.Hide();
        }
        else if (message.StartsWith("MessageReceived"))
        {
            string[] splitter = message.Split(' ');
            string messageString = message.Substring(splitter[0].Length).Trim();
            
            IChatMessage chatMessage = ChatMessage.FromString(messageString);
            _chatMessageStorage.AddMessage(User.CurrentChannel.Name, chatMessage);
            AddMessage(chatMessage);
        }
        else if (message.StartsWith("Channel"))
        {
            string[] splitter = message.Split(' ');
            
            if (message.Contains("joined"))
            {
                string channelName = splitter[1].Trim();
                IChannel channel = _chatChannelStorage.FindChannel(channelName);
                
                User.CurrentChannel = channel;
                LblChannel.Content = User.CurrentChannel.Name;

                List<IChatMessage> messages = _directory.Messages
                    .Where(chatMessage => chatMessage.ChannelName == User.CurrentChannel.Name).ToList();
                
                Messages.Items.Clear();
                foreach (IChatMessage chatMessage in messages)
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
                var cbxChannelType = _newChannel.FindControl<ComboBox>("CbxChannelType")!;
                
                if (cbxChannelType.SelectedIndex == 1)
                {
                    newChannel.IsPrivate = true;
                    
                    string user = splitter[5].TrimEnd('.');
                    IUser chatUser = _chatUserStorage.FindUser(user);
                    newChannel.AddUser(chatUser);
                }
                else
                {
                    List<IUser> users = _directory.Users;
                    foreach (IUser user in users)
                    {
                        if (user != User)
                        {
                            newChannel.AddUser(user);
                        }
                    }
                }
                
                newChannel.AddUser(User);
                _chatChannelStorage.AddChannel(newChannel);
                
                Channels.Items.Clear();
                foreach (IChannel channel in _directory.Channels)
                {
                    if (channel.Users.Contains(User))
                    {
                        Channels.Items.Add(channel);
                    }
                }
                
                ChannelsScrollViewer.ScrollToEnd();
                
                _cdlNewChannel.Hide();
                
                Writer.SendText($"/join Channel {newChannelName}");
            }
            else if (message.Contains("deleted"))
            {
                string channelName = splitter[1].Trim();
                IChannel removedChannel = _chatChannelStorage.FindChannel(channelName);
                
                if (User.CurrentChannel.Name == removedChannel.Name)
                {
                    Writer.SendText("/join Channel Anonymous");
                }
                
                _chatChannelStorage.RemoveChannel(removedChannel);
                
                Channels.Items.Clear();
                foreach (IChannel channel in _directory.Channels)
                {
                    Channels.Items.Add(channel);
                }
                
                ChannelsScrollViewer.ScrollToEnd();

                _cdlNewChannel.Hide();
            }
            else if (message.Contains("exists"))
            {
                var lblChannelExistsError = _newChannel.FindControl<Label>("LblChannelExistsError")!;
                
                lblChannelExistsError.IsVisible = true;
            }
        }
        else if (message.StartsWith("Error"))
        {
            if (message.Contains("password"))
            {
                LblLoginError.IsVisible = true;
            
                MaskedPassword = "";
                TbxPassword.Text = "";
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