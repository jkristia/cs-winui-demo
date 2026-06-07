using CommunityToolkit.Mvvm.ComponentModel;

namespace Nimbus.App.ViewModels.Tabs;

/// <summary>
/// Shared contract for the vertical analytics tabs. Each tab knows its own
/// label/glyph and loads its data lazily the first time it is shown.
/// </summary>
public abstract partial class TabViewModelBase : ObservableObject
{
    private bool _loaded;

    public abstract string Title { get; }
    public abstract string Glyph { get; }

    [ObservableProperty]
    private bool _isLoading;

    /// <summary>Loads data on first activation; subsequent calls are no-ops.</summary>
    public async Task EnsureLoadedAsync()
    {
        if (_loaded)
            return;

        IsLoading = true;
        await LoadAsync();
        _loaded = true;
        IsLoading = false;
    }

    protected abstract Task LoadAsync();

    /// <summary>Used as the accessible name of the tab in the list.</summary>
    public override string ToString() => Title;
}
