namespace PluginBase
{
    public struct AudioResponse
    {
        /// <summary>
        /// Audio file
        /// </summary>
        public string Audio;

        /// <summary>
        /// True of false, based on operation status
        /// </summary>
        public bool Success;

        /// <summary>
        /// Audio format, wav, mp3 tested and officially supported. Other formast might work as well
        /// </summary>
        public string AudioFormat = "mp3";

        /// <summary>
        /// Error message, it the Success was false
        /// </summary>
        public string ErrorMsg;

        /// <summary>
        /// Parameters used by the service to generate this image. It's not recommended just to add all there, filter out the ones that user might actually need, like seed or revised prompt
        /// </summary>
        public List<(string Key, string Value)> Params;

        public AudioResponse()
        {
        }
    }

    public interface IAudioPlugin : IPluginBase
    {
        /// <summary>
        /// Get new audio from whatever source
        /// Payload needs to be newtonsoft json serializable object. User can modify the payload, see DefaultPayload.
        /// When combaning track & item payloads, remeber those are references, so create new instances of them to prevent modifying original
        /// return ImageResponse
        /// </summary>
        public Task<AudioResponse> GetAudio(object trackPayload, object itemsPayload);
    }
}