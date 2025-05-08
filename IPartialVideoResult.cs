namespace PluginBase
{
    /// <summary>
    /// Inherit this if your video plugin provides partial resutls, like FramePack. Meaning it can output first 1sec etc
    /// </summary>
    public interface IPartialVideoFileResult
    {
        void SetPartialVideoFileResultCallback(Action<string> partialVideoResult);
    }
}