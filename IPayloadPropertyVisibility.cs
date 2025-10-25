namespace PluginBase
{
    /// <summary>
    /// Inherit track or item payload from this interface to enable property based visibility
    /// Function is called when something in iether of the payloads changes. This works for functions as well
    /// </summary>
    public interface IPayloadPropertyVisibility
    {
        /// <summary>
        /// If you need to react to some property change, that is initiated by user, this property is true for the duration of set (per value)
        /// </summary>
        public static bool UserInitiatedSet { get; set; }

        bool ShouldPropertyBeVisible(string propertyName, object trackPayload, object itemPayload);
    }
}