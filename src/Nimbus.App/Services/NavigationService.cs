using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace Nimbus.App.Services;

/// <summary>Frame-based implementation of <see cref="INavigationService"/>.</summary>
public sealed class NavigationService(IPageService pageService) : INavigationService
{
    private Frame? _frame;

    public event NavigatedEventHandler? Navigated;

    public Frame? Frame
    {
        get => _frame;
        set
        {
            if (_frame is not null)
                _frame.Navigated -= OnFrameNavigated;

            _frame = value;

            if (_frame is not null)
                _frame.Navigated += OnFrameNavigated;
        }
    }

    public bool CanGoBack => _frame?.CanGoBack ?? false;

    public bool NavigateTo(string key, object? parameter = null)
    {
        if (_frame is null)
            return false;

        var pageType = pageService.GetPageType(key);

        // Don't re-navigate to the page we're already showing.
        if (_frame.Content?.GetType() == pageType)
            return false;

        return _frame.Navigate(pageType, parameter, new EntranceNavigationTransitionInfo());
    }

    public bool GoBack()
    {
        if (!CanGoBack)
            return false;

        _frame!.GoBack();
        return true;
    }

    private void OnFrameNavigated(object sender, NavigationEventArgs e) =>
        Navigated?.Invoke(sender, e);
}
