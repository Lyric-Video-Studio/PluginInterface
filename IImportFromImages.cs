namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin supports making item payloads from image items, like img2img
    /// </summary>
    public interface IImportFromImage
    {
        /// <summary>
        /// Create new item payload based on image source. In stability video for example, this is used for generating the image.
        /// If your plugin does not need image source for the basis of video, you can just return same as DefaultPayloadForItem
        /// If you think this interface needs import feature from other tracks (like TxtToVideo, based on lyrics), please contact us
        /// </summary>
        public object ItemPayloadFromImageSource(string imgSource);
    }
}