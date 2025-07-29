namespace PluginBase
{
    /// <summary>
    /// Inherit track or item payload from this interface to enable property based visibility
    /// Function is called when something in iether of the payloads changes. This works for functions as well
    /// </summary>
    public interface IPayloadPropertyVisibility
    {
        bool ShouldPropertyBeVisible(string propertyName, object trackPayload, object itemPayload);
    }
}