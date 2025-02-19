﻿using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using JACService.Core.Contracts;
using JACService.Shared;

namespace JAC.Views;

public partial class NewChannel : UserControl
{
    #region constructor
    public NewChannel()
    {
        InitializeComponent();
    }
    #endregion
    
    #region methods
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
    
    private void CbxChannelType_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (CbxChannelType.SelectedIndex >= 0)
        {
            if (CbxChannelType.SelectedIndex == 1)
            {
                ChatDirectory chatDirectory = ChatDirectory.GetInstance();
                List<IUser> users = chatDirectory.Users;
            
                if (users.Count > 1)
                {
                    LblUser.IsVisible = true;
                    CbxUser.IsVisible = true;

                    foreach (IUser user in users)
                    {
                        if (user != MainView.User && !CbxUser.Items.Contains(user.Nickname))
                        {
                            CbxUser.Items.Add(user.Nickname);
                        }
                    }

                    CbxUser.SelectedIndex = 0;
                }
                else
                {
                    CbxChannelType.SelectedIndex = 0;
                }
            }
            else
            {
                LblUser.IsVisible = false;
                CbxUser.IsVisible = false;
            }
        }
        else
        {
            LblChannelTypeMissingError.IsVisible = true;
        }
    }
    
    private void BtnAddNewChannel_OnClick(object? sender, RoutedEventArgs e)
    {
        LblChannelMissingError.IsVisible = false;
        LblChannelTypeMissingError.IsVisible = false;
        LblChannelExistsError.IsVisible = false;
        
        if (!string.IsNullOrEmpty(TbxNewChannelName.Text))
        {
            if (CbxChannelType.SelectedIndex >= 0)
            {
                string channelType = CbxChannelType.SelectedIndex == 0 ? "public" : "private";
                string channelName = TbxNewChannelName.Text.Trim();

                if (channelType == "private")
                {
                    string user = (string)CbxUser.SelectedItem!;
                    MainView.Writer.SendText($"/create new {channelType} Channel {channelName} with User {user}");
                }
                else
                {
                    MainView.Writer.SendText($"/create new {channelType} Channel {channelName}");
                }
            }
            else
            {
                LblChannelTypeMissingError.IsVisible = true;
            }
        }
        else
        {
            LblChannelMissingError.IsVisible = true;
        }
    }
    #endregion
}