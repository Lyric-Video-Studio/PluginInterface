namespace PluginBase
{
    /// <summary>
    /// Inherit if validation depends on both payloads. For example track and item payloads contain list of items
    /// But only certain amount in total is ok
    /// </summary>
    public interface IValidateBothPayloads
    {
        public (bool payloadOk, string reasonIfNot) ValidatePayloads(object trackPaylod, object itemPayload);
    }
}