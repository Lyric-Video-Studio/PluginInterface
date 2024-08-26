using Microsoft.Maui.Storage;

namespace PluginBase
{
    public static class SecureStorageWrapper
    {
        public static string Get(string key)
        {
            Task<string> task = Task.Run(async () => await SecureStorage.Default.GetAsync(key));
            return task.Result;
        }

        public static void Set(string key, string value)
        {
            Task task = Task.Run(async () => await SecureStorage.Default.SetAsync(key, value));
            task.Wait();
        }
    } 
}
