namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin supports making item payloads from text, usually in txtToImg purposes.
    /// Or, your plugin could simply just render images with text
    /// </summary>
    public interface IImportFromLyrics
    {
        /// <summary>
        /// Create new item payload based on text. in A1111 case this create payload with positive prompt as text
        /// </summary>
        object ItemPayloadFromLyrics(string text);
    }

    /// <summary>
    /// INherit this if your payload support importing textual prompt whe importing images
    /// </summary>
    public interface IAppendLyrics
    {
        /// <summary>
        /// Create new item payload based on text. in A1111 case this create payload with positive prompt as text
        /// </summary>
        void AppendToPayloadFromLyrics(string text, object payload);
    }
}