# CLAUDE.md

Guidance for Claude Code when working in this repository.

## What this is

**Nimbus** — a WinUI 3 / .NET 8 desktop demo app. Unpackaged (runs as a plain `.exe`),
built and launched from VS Code; **no Visual Studio**. Fictional project & analytics
workspace used to showcase Fluent UI + clean MVVM.

## Solution layout

Two projects, deliberately split to enforce the UI/logic boundary:

- `src/Nimbus.Core` — plain `net8.0` class library. Models, `Data/MockDataStore.cs`
  (dummy data), and business-logic services. **Must never reference a UI type.**
- `src/Nimbus.App` — WinUI 3 app. Views, view-models, navigation, theming.

Data flow: `Views (no logic) → ViewModels → Nimbus.Core services → MockDataStore`.

## Build & run

Always pass the platform — the projects are x64-only.

```powershell
dotnet build Nimbus.sln -p:Platform=x64
# exe: src/Nimbus.App/bin/x64/Debug/net8.0-windows10.0.19041.0/win-x64/Nimbus.App.exe
```

In VS Code press **F5** (`.vscode/launch.json` runs the `build` task first).

## Architecture rules (keep these intact)

- **No logic in views.** Code-behind = `InitializeComponent`, resolve the VM from DI
  (`App.GetService<T>()`), and fire the load command. Nothing else.
- **Formatting/branching** goes in view-models or `Converters/`, never in XAML code-behind.
- View-models use **CommunityToolkit.Mvvm** (`[ObservableProperty]`, `[RelayCommand]`).
- DI is configured in `App.xaml.cs::ConfigureServices`. Core self-registers via
  `services.AddNimbusCore()`. Register new pages in both `ConfigureServices` and the
  `PageService` map; pages navigate by view-model type name (`typeof(XViewModel).FullName`).
- Reusable controls (`Controls/MetricCard`, `Controls/SettingRow`) are dependency-property
  driven and contain no logic.
- The analytics vertical tabs are a `ListView` + `ContentControl` + `Views/Tabs/TabTemplateSelector`,
  driven by `AnalyticsViewModel.SelectedTab`. Add a tab = new `TabViewModelBase` + a tab
  `UserControl` + a branch in the selector + DI registration + add to the `Tabs` collection.

## Environment gotchas (already solved — don't regress)

These were painful to discover; preserve the fixes:

1. **WindowsAppSDK version is pinned to `1.6.240829007`** with
   `<WindowsSdkPackageVersion>10.0.19041.57</WindowsSdkPackageVersion>` in
   `Nimbus.App.csproj`. The installed .NET 8.0.206 SDK ships an older Windows projection than
   newer WinAppSDK builds need (1.7/1.8 fail with `NETSDK1148`). Don't bump WinAppSDK without
   also raising that SDK package version, and verify it still builds.

2. **Do not put `required` members on any model bound in XAML.** The XAML type-info generator
   emits `new T()` activators; `required` makes them fail to compile (CS9035) and crashes the
   XAML compiler's pass 2 with no diagnostic. Use `init` properties with default initializers
   (`= ""`) instead.

3. **Inside `ItemsRepeater` item templates use `x:Bind` (with `x:DataType`), not `{Binding}`.**
   ItemsRepeater does not propagate `DataContext` to generated items, so `{Binding}` renders
   blank. `{Binding}` is fine for `ItemsSource` (resolves against the inherited DataContext).
   `x:Bind` can't stringify a non-string for `Text=`/`Run.Text=` — add a pre-formatted string
   property to the item record instead.

4. **No CommunityToolkit `SettingsControls`** — that package crashed this XamlCompiler version.
   Settings rows use the in-house `Controls/SettingRow`.

5. **Glyphs:** Segoe Fluent glyph characters typed into source are unreliable (sometimes
   stripped). In **C#**, set them deterministically (e.g. via PowerShell `[char]0xE80F`) and
   verify the codepoint; in **XAML** use entity refs like `&#xE80F;` (always reliable).

## Verifying UI changes

The app is a GUI, so "does it build" isn't enough. To confirm rendering: launch the exe and
capture the window with Win32 `PrintWindow` (flag `2` / PW_RENDERFULLCONTENT) — it works even
when the window isn't foreground. Drive navigation with **UI Automation**
(`SelectionItemPattern`/`InvokePattern`) rather than synthetic clicks, since the window often
isn't in the foreground.
