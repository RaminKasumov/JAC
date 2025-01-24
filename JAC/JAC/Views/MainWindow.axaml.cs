using FluentAvalonia.UI.Windowing;

namespace JAC.Views;

public partial class MainWindow : AppWindow
{
    #region constructor
    public MainWindow()
    {
        InitializeComponent();
        
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }
    #endregion
}