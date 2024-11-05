# PluginInterface
Public c# interface for creating new image/video plugins for Lyric Video Studio. More info coming up!!! See https://lyricvideo.studio

# Instructions
1. Clone this repository as submodule (for example)
2. Reference this project in your plugin
3. Build/publish your plugin
4. Copy plugin under 'plugins' in Lyric Video Studio and start the app

See build in plugins for examples on how to create plugins: 
https://github.com/Lyric-Video-Studio/BuildInPlugins
(note that these might not represent the best ways to develop things, lot's of these have been done in "mimimun viable product"-mentality. Also many of the interfaces provided by 3rd party services are lacking a bit)

Use contact form in https://lyricvideo.studio to request access to Microsoft Store beta "flight" list. Also, trial mode is coming in 0.9, it will support loading of 3rd party plugins.

Commands to publish this project / plugins (Tested and working)

dotnet publish .\PluginBase.csproj -c Release /p:RuntimeIdentifierOverride=win-x86 -o publish\plugins

dotnet publish .\Plugins\A1111Img2ImgPlugin\A1111Img2ImgPlugin.csproj -c Release /p:RuntimeIdentifierOverride=win-x86 -o publish\plugins
