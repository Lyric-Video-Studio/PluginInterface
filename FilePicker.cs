using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Threading;

namespace PluginBase
{
    public class FilePicker
    {
        private static IStorageProvider _w;

        public struct FilePickRes
        {
            public bool IsSuccessful { get; set; }
            public FilePick File { get; set; }
        }

        public struct FilePick
        {
            public Stream PathStream { get; set; }
            public List<Stream> PathStreams { get; set; }

            public string Path { get; set; }
            public string[] Paths { get; set; }
        }

        public static async Task<FilePickRes> PickAsync(string title, string initialPath = "", bool pickMany = false, string[] fileTypes = null)
        {
            var pickOptions = new FilePickerOpenOptions();
            pickOptions.AllowMultiple = false;
            pickOptions.Title = title;

            if (fileTypes != null)
            {
                var filter = new FilePickerFileType(string.Join(", ", fileTypes));
                filter.Patterns = fileTypes.Select(s => $"*{s}").ToList();
                pickOptions.FileTypeFilter = new List<FilePickerFileType>() { filter };
            }

            if (string.IsNullOrEmpty(initialPath))
            {
                var init = await _w.TryGetFolderFromPathAsync(Path.GetDirectoryName(initialPath));
                pickOptions.SuggestedStartLocation = init;
            }

            pickOptions.AllowMultiple = pickMany;

            IReadOnlyList<IStorageFile> res = null;

            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                res = await _w.OpenFilePickerAsync(pickOptions);
            });

            if (res.Any())
            {
#if WEB
                var mediaCachePath = "/pickedFiles/";
                var streams = new List<Stream>();
                var paths = new List<string>();
                var firstFile = "";

                foreach (var item in res)
                {
                    var newPath = $"{mediaCachePath}{Guid.NewGuid()}{Path.GetExtension(item.Name)}";
                    if (string.IsNullOrEmpty(firstFile))
                    {
                        firstFile = newPath;
                    }

                    streams.Add(await item.OpenReadAsync());
                    paths.Add(newPath);
                }
                // TODO: Can we append the file in smaller chunks?
                for (int i = 0; i < streams.Count; i++)
                {
                    using var memStream = new MemoryStream();
                    await streams[i].CopyToAsync(memStream);
                    await FileSystemWrapper.Instance.WriteFile(paths[i], memStream.ToArray());
                    streams[i].Dispose();
                }
                // Files need to be saved to cache

                return new FilePickRes() { IsSuccessful = true, File = new() { Path = firstFile, Paths = paths.ToArray() } };
#else

                var paths = pickMany ? res.Select(s => s.Path.LocalPath).ToArray() : [];
                return new FilePickRes() { IsSuccessful = true, File = new() { Path = res.First().Path.LocalPath, Paths = paths } };
#endif
            }
            {
                return new FilePickRes() { IsSuccessful = false };
            }
        }

        public static void SetWindow(IStorageProvider w)
        {
            _w = w;
        }
    }
}