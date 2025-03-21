namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin supports reporting progress. Progress and time left will be shown on payload editor. User might exit the view and check our other plugin / continue work
    /// so new progress action will be nulled on exit and new one given when re-entering
    /// </summary>
    public interface IProgressIndication
    {
        /// <summary>
        /// Application gives you progress action to invoke, when generation is progressing
        /// </summary>
        void SetProgressCallback(Action<(int currentProgress, int maxProgress)> action);
    }
}