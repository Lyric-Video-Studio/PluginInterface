﻿using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace PluginBase
{
    public class FilePicker
    {
        private static Window _w;

        public struct FilePickRes
        {
            public bool IsSuccessful { get; set; }
            public FilePick File { get; set; }
        }

        public struct FilePick
        {
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
                var init = await _w.StorageProvider.TryGetFolderFromPathAsync(Path.GetDirectoryName(initialPath));
                pickOptions.SuggestedStartLocation = init;
            }

            pickOptions.AllowMultiple = pickMany;

            var res = await _w.StorageProvider.OpenFilePickerAsync(pickOptions);

            if (res.Any())
            {
                var paths = pickMany ? res.Select(s => s.Path.LocalPath).ToArray() : [];
                return new FilePickRes() { IsSuccessful = true, File = new() { Path = res.First().Path.LocalPath, Paths = paths } };
            }
            {
                return new FilePickRes() { IsSuccessful = false };
            }
        }

        public static void SetWindow(Window w)
        {
            _w = w;
        }
    }
}