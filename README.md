# Lyric Video Studio Plugin Interface for C# Developers

`PluginInterface` is the public C# contract layer for building third-party Lyric Video Studio plugins.

If you want to add `image generation`, `video generation`, `audio generation`, `local AI tools`, or `custom media workflows` to Lyric Video Studio, this project is the integration surface you implement.

This README is written for developers only.

## What This Project Contains

The `PluginBase.csproj` project exposes the interfaces and helper attributes used by Lyric Video Studio to:

- discover plugin assemblies
- instantiate plugin classes
- render settings and payload editors
- validate inputs
- run image, video, and audio generation
- persist plugin state
- report progress back to the UI

Core contracts:

- `IPluginBase`
- `IImagePlugin`
- `IVideoPlugin`
- `IAudioPlugin`

Helper areas:

- dynamic editor attributes in [CustomAttributes.cs](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\PluginInterface\CustomAttributes.cs)
- payload serialization helpers in [JsonHelper.cs](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\PluginInterface\JsonHelper.cs)
- optional workflow interfaces such as `IRequestContentUploader`, `ISaveAndRefresh`, `IProgressIndication`, `ITextualProgressIndication`, `IValidateBothPayloads`, `IPluginEditUi`, and `ITrackPayloadFromModel`

## Target Framework

`PluginBase.csproj` currently targets `net10.0`.

If you want the simplest compatibility story, create your plugin as a C# class library targeting the same framework and reference [PluginBase.csproj](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\PluginInterface\PluginBase.csproj) directly.

## How Lyric Video Studio Loads Plugins

Lyric Video Studio scans:

- the app-local `plugins` folder
- the extra plugin folder configured in app settings

The loader reflects over plugin DLLs and creates instances from exported public concrete types that:

- implement `IImagePlugin`, `IVideoPlugin`, or `IAudioPlugin`
- are not abstract
- have a parameterless constructor

That means your plugin class should be:

- `public`
- concrete
- default-constructible
- packaged with any required dependency DLLs beside it

The app identifies plugins by `UniqueName`, so keep it stable and globally unique.

## The Minimum Plugin Shape

Every plugin ultimately implements `IPluginBase`, either directly or through one of:

- `IImagePlugin`
- `IVideoPlugin`
- `IAudioPlugin`

At minimum, your implementation needs to provide:

- stable identity via `UniqueName`
- display metadata via `DisplayName`, `SettingsHelpText`, and `SettingsLinks`
- a serializable general settings object
- default track and item payload objects
- payload copy, deserialize, and strong-type conversion methods
- initialization and connection cleanup
- validation
- file reference reporting and path replacement
- prompt/text extraction through `TextualRepresentation`
- Sign your plugin, at least with self-signed certificate. In near future, user gets warning if unsigned plugin is used. Signing the plugin isolates the API keys so that other plugins can no share it. Will be implemented soon

Generation methods:

- `IImagePlugin.GetImage(...)`
- `IVideoPlugin.GetVideo(...)`
- `IAudioPlugin.GetAudio(...)`

## Track Payload vs Item Payload

The architecture is built around two payload levels:

- track payload: shared settings for the whole track
- item payload: per-item prompts, overrides, references, or generation state

This split is one of the most important design choices when building a good plugin.

Typical pattern:

- track payload contains model, resolution, style, provider-wide defaults
- item payload contains prompt text, source media, polling IDs, and item-specific overrides

Your payload objects must be serializable and safe to clone. The app expects fresh instances from:

- `DefaultPayloadForTrack()`
- `DefaultPayloadForItem()`
- `CopyPayloadForTrack(...)`
- `CopyPayloadForItem(...)`

Do not return the same payload instance repeatedly.

## Dynamic Settings UI

Lyric Video Studio can build editors automatically from your public payload and settings properties.

Useful attributes from [CustomAttributes.cs](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\PluginInterface\CustomAttributes.cs):

- `Description`: tooltip/help text
- `EditorWidth`
- `EditorMaxHeigth`
- `EditorColumnSpan`
- `CustomName`
- `IgnoreDynamicEdit`
- `ForceShowOnDynamicEdit`
- `EnableFileDrop`
- `EnableFolderPicker`
- `MaskInput`
- `TriggerReload`
- `PropertyComboOptions`
- `ShowSlider`
- `HideAllChildren`
- `CustomAction`

This gives you a lot of UI without writing custom views.

## Supplying Selection Options

For combo-box style properties, implement:

- `IPropertyOptionsProvider.SelectionOptionsForProperty(...)`

For grouped or tree-like menus, implement:

- `IMenuSelectionOptionsForProperty.MenuSelectionOptionsForProperty(...)`

If your plugin needs to swap payload structure when a model changes, implement:

- `ITrackPayloadFromModel.TrackPayloadFromModel(...)`

That pattern is used in this repo for multi-model provider plugins.

## Optional Extension Interfaces

These interfaces are worth knowing early because they shape real-world plugin behavior:

- `IValidateBothPayloads`
  Used when track-level and item-level data must be validated together.

- `ISaveAndRefresh`
  Useful when generation mutates payload state, for example storing a polling ID and resuming later.

- `ISaveConnectionSettings`
  Useful when your settings object caches dynamic provider metadata such as model or voice lists.

- `IProgressIndication`
  Reports numeric progress to the editor.

- `ITextualProgressIndication`
  Reports provider states like `queued`, `processing`, or `uploading`.

- `ICancellableGeneration`
  Lets the app pass a cancellation token into your plugin workflow.

- `IRequestContentUploader`
  Lets your plugin ask Lyric Video Studio to upload local media to a public URL when a provider requires publicly reachable assets.

- `IImportFromImage`, `IImportFromVideo`, `IImportFromVideoFrames`, `IImportContentId`
  Enables richer creation flows from existing media or previous provider outputs.

- `IPluginEditUi`
  Lets you replace the autogenerated payload editor with a custom Avalonia `UserControl`.

- `IGenerationCost`
  Surface estimated generation cost in the UI when your provider supports it.

- `IContentId`
  Expose provider-side IDs for follow-up operations like upscale, extend, or lip sync.

## File References and Project Portability

Implement these carefully:

- `FilePathsOnPayloads(...)`
- `ReplaceFilePathsOnPayloads(...)`

They are what make project archiving, path remapping, and media relocation work correctly.

If your payload stores local file paths, report all of them here.

## Secure Settings and API Keys

The official FAQ says Lyric Video Studio stores plugin keys in Windows Credential Manager and does not log them in plain text.

For plugin authors, that means:

- keep secrets in your settings object only as needed
- use the provided secure-storage abstractions where appropriate
- mark key fields with `MaskInput`
- avoid persisting raw secrets unnecessarily inside track or item payloads

## Build and Publish Workflow

Typical flow:

1. Create a class library plugin project
2. Reference [PluginBase.csproj]
3. Implement one or more plugin interfaces
4. Publish your plugin and its dependencies to a folder
5. Copy the output under a Lyric Video Studio scanned plugin folder
6. Restart the app and test initialization from plugin settings

Example publish command for the interface project:

```powershell
dotnet publish .\PluginBase.csproj -c Release -o publish\plugins
```

Example pattern for your plugin project:

```powershell
dotnet publish .\MyPlugin\MyPlugin.csproj -c Release -o publish\plugins
```

## Development and Testing Tips

- Start by copying patterns from the plugins under [BuildInPlugins](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins)
- Keep `UniqueName` stable once users may save projects with your plugin
- Use `Description` attributes aggressively; good tooltips reduce support load
- Validate before making provider calls so users get immediate feedback
- If your provider returns long-running jobs, persist polling IDs and support resume behavior
- If your provider needs public URLs, use `IRequestContentUploader` rather than storing temporary URLs directly in payloads
- Return filtered, user-meaningful params in `ImageResponse`, `VideoResponse`, or `AudioResponse` instead of dumping full provider payloads

## Testing Environment Notes

From the official site:

- Steam does not ship with built-in plugins, so it is a useful environment for testing external plugin deployment
- The Microsoft Store LITE edition does not load arbitrary third-party plugins unless whitelisted
- The plugin page invites developers to make contact if they want their plugin featured

If you need a full app build for plugin development, the official plugin page also mentions contacting the author about beta-build access.

## Reference Implementations

Use these as concrete examples:

- [../Google](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\Google)
- [../FalAi](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\FalAi)
- [../RunwayMl](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\RunwayMl)
- [../MuApi](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\MuApi)
- [../CroppedImagePlugin](t:\CodeProjects\LyricVIdeoSyncer\BuildInPlugins\CroppedImagePlugin)

The examples are real production integrations, so they are more useful than a toy sample even if some were clearly built with pragmatic MVP tradeoffs.

## Useful Links

- Main site: `https://lyricvideo.studio/`
- Plugins page: `https://lyricvideo.studio/plugins/`
- Features: `https://lyricvideo.studio/features/`
- FAQ: `https://lyricvideo.studio/f-a-q/`
- Built-in plugin examples: `https://github.com/Lyric-Video-Studio/BuildInPlugins`

If you are building a provider integration for Lyric Video Studio, this project is the contract. The rest of the work is good payload design, careful validation, and making the provider feel native inside the editor.
