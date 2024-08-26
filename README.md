# PluginInterface
Public c# interface for creating new image/video plugins for Lyric Video Studio. More info coming up!!! See https://lyricvideo.studio

# Instructions
1. Clone this repository as submodule (for example)
2. Reference this project in your plugin
3. Build/publish your plugin
4. Copy plugin under 'plugins' in Lyric Video Studio and start the app

More info coming up!!!

Commands to publish this project / plugins (Tested and working)
dotnet publish .\PluginBase.csproj -c Release /p:RuntimeIdentifierOverride=win-x86 -o publish\plugins
dotnet publish .\Plugins\A1111Img2ImgPlugin\A1111Img2ImgPlugin.csproj -c Release /p:RuntimeIdentifierOverride=win-x86 -o publish\plugins
