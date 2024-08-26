namespace PluginBase
{
    /// <summary>
    /// Inherit from this if your video plugin supports importing existing video frames. Usually like upscaling or vid2vid.
    /// </summary>
    public interface IImportFromVideoFrames
    {
        /// <summary>
        /// Create new item payload based on frames. Given frames are absolute paths source frames, extracted by the app
        /// </summary>
        public object ItemPayloadFromImageSequence(List<string> frames);
    }
}