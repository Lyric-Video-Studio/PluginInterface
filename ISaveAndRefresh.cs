namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin modifies original payloads during generation. Use case: service returns polling id which will be valid for 24h
    /// User could shut down the whole app and then restart it later, if there's been noticeable delay / queue in the service. Your plugin should be able to continue polling
    /// for the results, instead of requesting new image/video. Usually just for video generation servives. Your plugin should clear the polling id when succesfully saved the video file.
    /// User can manually remove it also
    /// </summary>
    public interface ISaveAndRefresh
    {
        void SetSaveAndRefreshCallback(Action saveAndRefreshCallback);
    }
}