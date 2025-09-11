using System.Text;

namespace PluginBase
{
    public class FileSystemWrapper
    {
        public static IFileSystemWrapper Instance { get; set; }

        public static async Task Delete(string finalPath)
        {
            await Instance.DeleteAsync(finalPath);
        }

        public static async Task<bool> Exists(string finalPath)
        {
            return await Instance.Exist(finalPath);
        }

        public static async Task WriteAllText(string path, string output)
        {
            await Instance.WriteFile(path, Encoding.UTF8.GetBytes(output), output);
        }
    }

    public interface IFileSystemWrapper
    {
        bool Initialized { get; set; }

        Task<int> Initialize();

        Task<string> ReadAllTextAsync(string path);

        Task<byte[]> ReadAllAsync(string path);

        Task DeleteAsync(string path);

        Task<bool> Exist(string path);

        Task CreateDirectoryAsync(string path);

        Task WriteFile(string path, byte[] contents, string rawString = "");

        Task<(long Usage, long Quota)?> CheckAvailableSpaceAsync();

        Task PreInit();
    }
}