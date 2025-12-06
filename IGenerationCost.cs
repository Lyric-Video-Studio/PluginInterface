namespace PluginBase
{
    /// <summary>
    /// Inherit if plugin supports showing cost of generation. Preferrably before the generate click, if possible :)
    /// </summary>
    public interface IGenerationCost
    {
        /// <summary>
        /// Invoke this action when cost changes and ui will update for the cost label
        /// </summary>
        public void SetShowCostAction(Action<string> cost);
    }
}