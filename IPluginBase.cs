using System.Text.Json.Nodes;

namespace PluginBase
{
    public static class WorkspaceSettings
    {
        /// <summary>
        /// Do not modify this. This is set when oroject has been loaded. Paths can be relative to this, so whenever dealing with images for example, check this
        /// </summary>
        public static string CurrentProjectPath;

        public static string GetAbsolutePath(string path, bool forceAbsolute = false)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (Path.IsPathRooted(path))
            {
                return path;
            }

            if (File.Exists(path) && !forceAbsolute)
            {
                return path;
            }

            if (string.IsNullOrEmpty(CurrentProjectPath))
            {
                return "";
            }

            return Path.Combine(Path.GetDirectoryName(CurrentProjectPath), path);
        }
    }

    /// <summary>
    /// Base interface for plugins, provides common functions needed for all plugins. Any parameter editing is done on app ui OR with ui provided by the plugin. App ui finds editable properties dynamically, if built-in ui is used,
    /// and fills the editor with public/writeable properties. You can use these attributes to manipulate editing ui.
    /// Property names should be human readable and with CamelCase: ui will add spaces based on that, so user will see "Camel case" as name of the attribute
    /// 1. [DescriptionAttribute] = provide tooltip for parameter
    /// 2. [EditorWidth] = modify default width of editor item, value is applied to .WidthRequest if provided
    /// 3. [EditorHeigth] = modify default maximum heigth of editor item, value is applied to .MaximumHeightRequest if provided
    /// 3. [Columnspan] = Add columnspan to item so it will more colums
    /// </summary>
    public interface IPluginBase
    {
        public enum TrackType
        {
            Image,
            Video,
            Audio
        }

        /// <summary>
        /// Unique name of the plugin. Make sure it's unique enough, because plugin is loaded based of this name.
        /// </summary>
        public string UniqueName { get; }

        /// <summary>
        /// User friendly display name
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Instructions to show on plugin default settings page
        /// </summary>
        public string SettingsHelpText { get; }

        /// <summary>
        /// http links to external resources if needed
        /// </summary>
        public string[] SettingsLinks { get; }

        /// <summary>
        /// Tells the app if multiple items of same track can be generated at the same time. App will pause for 2sec before calling next one, to avoid throttle limits to exceed
        /// If plugin generates stuff on could, usually this should be set to true. In case of local generating, false might be required
        /// </summary>
        public bool AsynchronousGeneration { get; }

        /// <summary>
        /// Initialized plugin so that it's ready for use. Return empty (or null) string if everything is ok, user is displayable message if not
        /// See also GeneralSettings function, object should match it. Should not make any connections outside in this function yet (preferred).
        /// Remember that GetVideo and GetImage functions should not rely to any instance properties, because the function will be called multiple times simultaneously
        /// </summary>
        public Task<string> Initialize(object settings);

        /// <summary>
        /// Button in ui to test general setting (or otherwise)
        /// </summary>
        public Task<string> TestInitialization();

        /// <summary>
        /// Settings object that is serializable with newtonsoft json. These settings are showed in Settings menu.
        /// These settings should include such settigs than whenever user uses this plugin, it does not have to fill same details avery time
        /// Good example is local Automatic1111 host address. Object must be same than in Initialize. Do not return reference to same object, otherwise editing one track settings
        /// can lead to editing others as well. Setting prperties can have [Description]-attribute for showing tooltip in ui
        /// </summary>
        public object GeneralDefaultSettings { get; }

        /// <summary>
        /// Close any open connections & cleanup any resources. Called either when project closes of tested from settings view
        /// </summary>
        public void CloseConnection();

        /// <summary>
        /// Return status on initialization, main app may ask this and then initialize the plugin "lazy"
        /// </summary>
        public bool IsInitialized { get; }

        /// <summary>
        /// Return selection options if property should be shown as combobox / selection. Return null or empty list if no selection options for property
        /// </summary>
        public Task<string[]> SelectionOptionsForProperty(string propertyName);

        /// <summary>
        /// Create new instance. This instance can have different settings and different intances may be active of different tracks
        /// </summary>
        public IPluginBase CreateNewInstance();

        /// <summary>
        /// Deserialize track payload preset, plugin knows it's correct type
        /// </summary>
        public object DeserializePayload(string fileName);

        /// <summary>
        /// Convert deserialized object to strongly typed. This is due to system.text.json restrictions. Your plugin is only one who knows the exact type
        /// </summary>
        public object ObjectToItemPayload(JsonObject obj);

        /// <summary>
        /// Convert deserialized object to strongly typed. This is due to system.text.json restrictions. Your plugin is only one who knows the exact type
        /// </summary>
        public object ObjectToTrackPayload(JsonObject obj);

        /// <summary>
        /// Convert deserialized object to strongly typed. This is due to system.text.json restrictions. Your plugin is only one who knows the exact type
        /// </summary>
        public object ObjectToGeneralSettings(JsonObject obj);

        /// <summary>
        /// Sets the type of track this plugin is instantiated to. Used when same plugin can handle multiple types
        /// </summary>
        public TrackType CurrentTrackType { get; set; }

        /// <summary>
        /// Return the text part of the payload, usually prompt. This will be displayed on item in timeline. Don't include any source paths to it, just return empty if this function does not matter to you
        /// </summary>
        public string TextualRepresentation(object itemPayload);

        /// <summary>
        /// Get default payload for track. It's up to you how you combine this info. In general, track should have most of the information and item only the specific ones
        /// Use [Description] attribute to provide tooltip for payload property if needed. Example: Automatic111 txtToImg, track has most variables that is needed for generation
        /// and item has the specific prompt with other small finatunigs. Do not return reference to same object, otherwise editing one track settings
        /// can lead to editing others as well. TrackType is alwatys set before this is called, so if your plugin supports multiple outputs, use that enum
        /// </summary>
        public object DefaultPayloadForTrack();

        /// <summary>
        /// Get default payload. It's up to you how you combine this info. In general, track should have most of the information and item only the specific ones
        /// Use [Description] argument to provide tooltip for payload property if needed. Do not return reference to same object, otherwise editing one track settings
        /// can lead to editing others as well,  TrackType is alwatys set before this is called, so if your plugin supports multiple outputs, use that enum
        /// </summary>
        public object DefaultPayloadForItem();
    }
}