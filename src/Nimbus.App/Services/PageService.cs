using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace Nimbus.App.Services;

/// <summary>
/// Holds the view-model -> page association. Using the view-model's type name
/// as the navigation key keeps the views free of any page-type knowledge.
/// </summary>
public sealed class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public void Register<TViewModel, TView>()
        where TViewModel : ObservableObject
        where TView : Page
    {
        var key = typeof(TViewModel).FullName!;
        _pages[key] = typeof(TView);
    }

    public Type GetPageType(string key) =>
        _pages.TryGetValue(key, out var pageType)
            ? pageType
            : throw new ArgumentException($"No page registered for key '{key}'.", nameof(key));
}
