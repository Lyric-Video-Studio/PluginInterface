namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin supports cancelling of generation process. Usually with cloud based generation, cancelling is not very usefull
    /// However, if your plugin works by splitting the process somehow (like manipulating one frame at the time), then it's wise to be able to cancel
    /// Token is set before calling GetVideo (at leat for now, generating images is not cancellable, expect when generating multiple timeline items, but that's internal magic)
    /// </summary>
    public interface ICancellableGeneration
    {
        void SetCancallationToken(CancellationToken cancellationToken);
    }
}