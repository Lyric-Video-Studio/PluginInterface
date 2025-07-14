namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin modifies original payloads during generation. Use case: service returns polling id which will be valid for 24h
    /// User could shut down the whole app and then restart it later, if there's been noticeable delay / queue in the service. Your plugin should be able to continue polling
    /// for the results, instead of requesting new image/video. Usually just for video generation servives. Your plugin should clear the polling id when succesfully saved the video file.
    /// User can manually remove it also. You can add this interface to your payload as well, to trigger refreshing of the screen
    /// </summary>
    public interface ISaveAndRefresh
    {
        void SetSaveAndRefreshCallback(Action saveAndRefreshCallback);
    }

    /// <summary>
    /// Inherit this if your plugin modifies connection settings. Connection settigs class can be used for caching data, for example. 
    /// For example, your plugins gets some dynamic data for comboboxes, it might not be neede to refresh on every startup
    /// </summary>
    public interface ISaveConnectionSettings
    {
        void SetSaveConnectionSettingsCallback(Action<object> saveConnectionSettings);
    }
}