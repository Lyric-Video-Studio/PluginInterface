using Meziantou.Framework.Win32;

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

            /*Task<string> task = Task.Run(async () => await SecureStorage.Default.GetAsync(key));
            return task.Result;*/
            return CredentialManager.ReadCredential(key)?.Password;
        }

        public static void Set(string key, string value)
        {
            if (IsRendering)
            {
                // Will crash the app if this is called on project rendering, when copies of the project is made
                return;
            }
            CredentialManager.WriteCredential(key, "", value, CredentialPersistence.LocalMachine);
            /*Task task = Task.Run(async () => await SecureStorage.Default.SetAsync(key, value));
            task.Wait();*/
        }
    }
}