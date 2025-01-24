using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace JAC.Views;

public partial class AccountOptions : UserControl
{
    #region constructor
    public AccountOptions()
    {
        InitializeComponent();
        
        TbxName.Text = MainView.User.Nickname;
        TbxPasscode.Text = new string('*', MainView.MaskedPassword.Length);
    }
    #endregion
    
    #region methods
    private void TbxName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        TblPlaceHolderName.IsVisible = string.IsNullOrEmpty(TbxName.Text);
    }
    
    private void TbxPasscode_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        TblPlaceHolderPasscode.IsVisible = string.IsNullOrEmpty(TbxPasscode.Text);
    }
    
    private void BtnShowPasscode_OnClick(object sender, RoutedEventArgs e)
    {
        TbxPasscode.Text = MainView.MaskedPassword;
        
        BtnShowPasscode.IsVisible = false;
        BtnHidePasscode.IsVisible = true;
    }

    private void BtnHidePasscode_OnClick(object sender, RoutedEventArgs e)
    {
        TbxPasscode.Text = new string('*', MainView.MaskedPassword.Length);
        
        BtnShowPasscode.IsVisible = true;
        BtnHidePasscode.IsVisible = false;
    }
    
    private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
    {
        using Stream stream = new FileStream($"{MainView.User.Nickname}.json", FileMode.Create, FileAccess.Write);
        MainView.User.WriteJson(stream);
        stream.Close();
        
        MainView.Writer.SendText($"/logout {MainView.User.Nickname}");
    }
    #endregion
}