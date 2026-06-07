using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Nimbus.App.Services;

/// <summary>Drives the top-level content frame from view-model keys.</summary>
public interface INavigationService
{
    /// <summary>The frame this service drives. Set once by the shell.</summary>
    Frame? Frame { get; set; }

    bool CanGoBack { get; }

    event NavigatedEventHandler? Navigated;

    /// <summary>Navigates to the page registered for <paramref name="key"/>.</summary>
    bool NavigateTo(string key, object? parameter = null);

    bool GoBack();
}
