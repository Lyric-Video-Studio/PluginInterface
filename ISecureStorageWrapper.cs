namespace PluginBase
{
    /// <summary>
    /// This wrapper hides the implementation on LVS secure storage. This is for future use, DO NOT USE THIS YET!!!s
    /// </summary>

    public interface ISecureStorageWrapper
    {
        public bool IsRendering { get; set; }

        public string GetKey(string key);

        public void SetKey(string key, string value);

        public void DeleteKey(string key);
    }
}