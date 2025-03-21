namespace PluginBase
{
    public interface IContentId
    {
        /// <summary>
        /// If your plugin has some sort of generation / polling id and it might be usefull to other application, use this interface to provide "Copy content id"-context menu in item context menu
        /// </summary>
        public string GetContentFromPayloadId(object payload);
    }
}