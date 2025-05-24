
namespace PluginBase
{
    /// <summary>
    /// This wrapper hides the implementation on LVS secure storage. This is for future use, DO NOT USE THIS YET!!!s
    /// </summary>

    public interface ISecureStorageWrapper
    {
        public bool IsRendering {get;set;}
        public string Get(string key);
        public void Set(string key, string value);
    }
}