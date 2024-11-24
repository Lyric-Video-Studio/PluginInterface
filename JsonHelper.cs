using System.Text.Json;

namespace PluginBase
{
    public static class JsonHelper
    {
        public static List<string> Errors = new List<string>();

        public static T DeserializeString<T>(string obj)
        {
            var output = JsonSerializer.Deserialize<T>(obj, GetSettings());
            return output;
        }

        public static T Deserialize<T>(string path)
        {
            return DeserializeString<T>(File.ReadAllText(path));
        }

        public static string Serialize<T>(T obj)
        {
            var output = JsonSerializer.Serialize(obj, GetSettings());
            return output;
        }

        public static void SerializeToPath<T>(T obj, string path)
        {
            var output = JsonSerializer.Serialize(obj, GetSettings());
            File.WriteAllText(path, output);
        }

        public static T DeepCopy<T>(object obj)
        {
            return DeserializeString<T>(Serialize(obj));
        }

        private static JsonSerializerOptions settings;

        public static JsonSerializerOptions GetSettings()
        {
            if (settings == null)
            {
                settings = new JsonSerializerOptions() { WriteIndented = true };
                /*settings.Error = delegate (object? sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    Errors.Add(args.ErrorContext.Error.ToString());
                    args.ErrorContext.Handled = true;
                };*/
            }
            return settings;
        }
    }
}