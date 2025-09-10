using System.Text;

namespace PluginBase
{
    public class FileSystemWrapper
    {
        public static IFileSystemWrapper Instance { get; set; }

        internal static void WriteAllText(string path, string output)
        {
            Instance.WriteFile(path, Encoding.UTF8.GetBytes(output));
        }
    }

    public interface IFileSystemWrapper
    {
        bool Initialized { get; set; }

        Task<int> Initialize();

        string ReadAllText(string path);

        Task<string> ReadAllTextAsync(string path);

        byte[] ReadAll(string path);

        Task<byte[]> ReadAllAsync(string path);

        void Delete(string path);

        Task DeleteAsync(string path);

        void Exist(string path);

        Task CreateDirectoryAsync(string path);

        Task WriteFile(string path, byte[] contents);

        Task<(long Usage, long Quota)?> CheckAvailableSpaceAsync();
    }
}