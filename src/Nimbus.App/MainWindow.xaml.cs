using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nimbus.App.Views;
using Windows.Graphics;

namespace Nimbus.App;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Title = "Nimbus";
        SystemBackdrop = new MicaBackdrop();
        ExtendsContentIntoTitleBar = true;

        GetAppWindow().Resize(new SizeInt32(1280, 840));
    }

    /// <summary>
    /// Loads the shell. Called after <see cref="App.MainWindow"/> is assigned so the
    /// shell can safely reach the window (e.g. to set the custom title bar).
    /// </summary>
    public void NavigateToShell() => RootFrame.Navigate(typeof(ShellPage));

    private AppWindow GetAppWindow()
    {
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(windowId);
    }
}
