namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin supports reporting progress in form text
    /// </summary>
    public interface ITextualProgressIndication
    {
        /// <summary>
        /// Application gives you textual progress action to invoke. Use it if (video) service provides some state info, like "queuing", "processing".
        /// Currently is shown only for video plugins in edit view
        /// </summary>
        void SetTextProgressCallback(Action<string> action);
    }
}