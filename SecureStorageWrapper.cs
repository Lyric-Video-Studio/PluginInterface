using Microsoft.Maui.Storage;

namespace PluginBase
{
    public static class SecureStorageWrapper
    {
        public static bool IsRendering;

        public static string Get(string key)
        {
            if (IsRendering)
            {
                // Will crash the app if this is called on project rendering, when copies of the project is made
                return "";
            }
            Task<string> task = Task.Run(async () => await SecureStorage.Default.GetAsync(key));
            return task.Result;
        }

        public static void Set(string key, string value)
        {
            if (IsRendering)
            {
                // Will crash the app if this is called on project rendering, when copies of the project is made
                return;
            }
            Task task = Task.Run(async () => await SecureStorage.Default.SetAsync(key, value));
            task.Wait();
        }
    }
}