using Microsoft.UI.Xaml.Controls;

namespace Nimbus.App.Services;

/// <summary>Maps a view-model key to the page type that renders it.</summary>
public interface IPageService
{
    Type GetPageType(string key);
}
