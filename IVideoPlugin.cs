namespace PluginBase
{
    public struct VideoResponse
    {
        /// <summary>
        /// Path to file, if video file was writen to disk. Leave empty/null if frames were stored as single frames to disk to folder given in GetVideo
        /// When returning just frames, it is important that frames are numbered in a way that video library can regognize them, 1.jpg, 2.jpg WILL NOT DO.
        /// use frameIndex.ToString(CommonConstants.DefaultIntToStringFormat) + ".jpg"; for example to make sure the files are like 000000001.jpg
        /// If leaving this empty, ImageFormat must be set!!!
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
        /// Fps of the image seqence, 0 = use project framerate
        /// </summary>
        public double Fps;

        /// <summary>
        /// Path to video mask. Optional. App will apply the mask automatically
        /// </summary>
        public string VideoMask;

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
    }
}