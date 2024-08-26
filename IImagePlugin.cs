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

        /// <summary>
        /// Get default payload for track. It's up to you how you combine this info. In general, track should have most of the information and item only the specific ones
        /// Use [Description] attribute to provide tooltip for payload property if needed. Example: Automatic111 txtToImg, track has most variables that is needed for generation
        /// and item has the specific prompt with other small finatunigs. Do not return reference to same object, otherwise editing one track settings
        /// can lead to editing others as well
        /// </summary>
        public object DefaultPayloadForImageTrack();

        /// <summary>
        /// Get default payload. It's up to you how you combine this info. In general, track should have most of the information and item only the specific ones
        /// Use [Description] argument to provide tooltip for payload property if needed. Do not return reference to same object, otherwise editing one track settings
        /// can lead to editing others as well
        /// </summary>
        public object DefaultPayloadForImageItem();

        /// <summary>
        /// Get copy of payload. This is needed because main app does not know the object type.
        /// </summary>
        public object CopyPayloadForImageTrack(object obj);

        /// <summary>
        /// Get copy of payload. This is needed because main app does not know the object type.
        /// </summary>
        public object CopyPayloadForImageItem(object obj);

        /// <summary>
        /// Validate item payload. This is important for example in img2vid, where image must be of certain size
        /// Validating payload reduces traffic to servers. Validation is done when item is opened or some of the values change.
        /// User interface does not prevent user pressing "generate" button for invalid payload, but the plugin can do that if it wishes so
        /// </summary>
        public (bool payloadOk, string reasonIfNot) ValidateImagePayload(object payload);
    }
}