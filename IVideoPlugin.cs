namespace PluginBase
{
    public struct VideoResponse
    {
        /// <summary>
        /// Path to file, if video file was writen to disk. Leave empty/null if frames were stored as single frames to disk to folder given in GetVideo
        /// When returning just frames, it is important that frames are numbered in a way that video library can regognize them, 1.jpg, 2.jpg WILL NOT DO.
        /// use frameIndex.ToString(CommonConstants.DefaultIntToStringFormat) + ".jpg"; for example to make sure the files are like 000000001.jpg
        /// </summary>
        public string VideoFile;

        /// <summary>
        /// Image format, if frames are exported instead of video file. Commonly used format, like png & jpg
        /// </summary>
        public string ImageFormat;

        /// <summary>
        /// True of false, based on operation status
        /// </summary>
        public bool Success;

        /// <summary>
        /// Error message, it the Success was false
        /// </summary>
        public string ErrorMsg;

        /// <summary>
        /// Parameters used by the service to generate this image. It's not recommended just to add all there, filter out the ones that user might actually need, like seed or revised prompt
        /// </summary>
        public List<(string Key, string Value)> Params;

        public VideoResponse()
        {
        }
    }

    public interface IVideoPlugin : IPluginBase
    {
        /// <summary>
        /// Get new video
        /// Payload needs to be newtonsoft json serializable object. User can modify the payload, see DefaultPayload.
        /// When combining track & item payloads, remember those are references, so create new instances of them to prevent modifying original
        /// folderToSaveVideo = path for saving the video file, response should have the full path to saved file
        /// When writing video file to disk, use unique file name, preferrably just Guid.NewGuid().ToString().mp4 for example, application keeps
        /// track of previous generating results & prompts, so user shoúld ge able to get previous results without regenerating them
        /// return VideoResponse
        /// </summary>
        public Task<VideoResponse> GetVideo(object trackPayload, object itemsPayload, string folderToSaveVideo);

        /// <summary>
        /// Get default payload for track. It's up to you how you combine this info. In general, track should have most of the information and item only the specific ones
        /// Use [Description] attribute to provide tooltip for payload property if needed. Example: Automatic1111 txtToImg, track has most variables that is needed for generation
        /// and item has the specific prompt with other small finetunigs. Do not return reference to same object, otherwise editing one track settings
        /// can lead to editing others as well
        /// </summary>
        public object DefaultPayloadForVideoTrack();

        /// <summary>
        /// Get default payload. It's up to you how you combine this info. In general, track should have most of the information and item only the specific ones
        /// Use [Description] argument to provide tooltip for payload property if needed. Do not return reference to same object, otherwise editing one track settings
        /// can lead to editing others as well
        /// </summary>
        public object DefaultPayloadForVideoItem();

        /// <summary>
        /// Get copy of payload. This is needed because main app does not know the object type.
        /// </summary>
        public object CopyPayloadForVideoTrack(object obj);

        /// <summary>
        /// Get copy of payload. This is needed because main app does not know the object type.
        /// </summary>
        public object CopyPayloadForVideoItem(object obj);

        /// <summary>
        /// Validate item payload. This is important for example in img2vid, where image must be of certain size
        /// Validating payload reduces traffic to servers. Validation is done when item is opened or some of the values change.
        /// User interface does not prevent user pressing "generate" button for invalid payload, but the plugin can do that if it wishes so
        /// </summary>
        public (bool payloadOk, string reasonIfNot) ValidateVideoPayload(object payload);
    }
}