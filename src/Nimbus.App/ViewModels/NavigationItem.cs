namespace Nimbus.App.ViewModels;

/// <summary>A single entry in the shell's navigation rail.</summary>
/// <param name="Label">Text shown in the pane.</param>
/// <param name="Glyph">Segoe Fluent Icons glyph.</param>
/// <param name="Key">View-model type name used as the navigation key.</param>
public sealed record NavigationItem(string Label, string Glyph, string Key);
