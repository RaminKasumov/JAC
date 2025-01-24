using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

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
    
    private void BtnAddNewChannel_OnClick(object? sender, RoutedEventArgs e)
    {
        LblChannelInputError.IsVisible = false;
        LblChannelExistsError.IsVisible = false;
        LblUserError.IsVisible = false;
        
        if (!string.IsNullOrEmpty(TbxNewChannelName.Text))
        {
            string channelType = CbxChannelType.SelectedIndex == 0 ? "public" : "private";
            string channelName = TbxNewChannelName.Text.Trim();
            
            if (CbxChannelType.SelectedIndex == 0)
            {
                MainView.Writer.SendText($"/create new {channelType} Channel {channelName}");
            }
            else
            {
                MainView.Writer.SendText($"/create new {channelType} Channel {channelName}");
            }
        }
        else
        {
            LblChannelInputError.IsVisible = true;
        }
    }
    #endregion
}