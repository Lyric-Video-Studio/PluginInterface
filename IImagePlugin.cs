namespace PluginBase
{
    public struct ImageResponse
    {
        /// <summary>
        /// Image as base64 encoded string
        /// </summary>
        public string Image;

        /// <summary>
        /// True of false, based on operation status
        /// </summary>
        public bool Success;

        /// <summary>
        /// Image format. Currently png & jpg are officially supported & tested
        /// </summary>
        public string ImageFormat = "jpg";

        /// <summary>
        /// Error message, it the Success was false
        /// </summary>
        public string ErrorMsg;

        /// <summary>
        /// Parameters used by the service to generate this image. It's not recommended just to add all there, filter out the ones that user might actually need, like seed or revised prompt
        /// </summary>
        public List<(string Key, string Value)> Params;

        public ImageResponse()
        {
        }
    }

    public interface IImagePlugin : IPluginBase
    {
        /// <summary>
        /// Get new image from whatever source
        /// Payload needs to be newtonsoft json serializable object. User can modify the payload, see DefaultPayload.
        /// When combaning track & item payloads, remeber those are references, so create new instances of them to prevent modifying original
        /// return ImageResponse
        /// </summary>
        public Task<ImageResponse> GetImage(object trackPayload, object itemsPayload);
    }
}