using Meziantou.Framework.Win32;

namespace PluginBase
{
    public static class SecureStorageWrapper
    {
        // WIP! Do not use this yet. This will be null on windows ;)
        public static ISecureStorageWrapper SecStorage { get; set; }

        private static bool _isRendering;
        public static bool IsRendering
        {
            get
            {
                if (SecStorage != null)
                {
                    return SecStorage.IsRendering;
                }
                return _isRendering;
            }

            set
            {
                if (SecStorage != null)
                {
                    SecStorage.IsRendering = value;
                }
                _isRendering = value; ;
            }
        }

        public static string Get(string key)
        {
            if (SecStorage != null)
            {
                return SecStorage.Get(key);
            }
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
            if (SecStorage != null)
            {
                SecStorage.Set(key, value);
                return;
            }
            if (IsRendering || value == null)
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