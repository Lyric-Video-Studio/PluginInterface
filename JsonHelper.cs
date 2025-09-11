using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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

        public static async Task<T> DeserializeAsync<T>(string path)
        {
            return DeserializeString<T>(await ReadAllText(path));
        }

        public static T Deserialize<T>(string path)
        {
            return DeserializeString<T>(ReadAllText(path).Result);
        }

        public static string Serialize<T>(T obj)
        {
            var output = JsonSerializer.Serialize(obj, GetSettings()).Replace(@"\u0026", "&");
            return output;
        }

        public static void SerializeToPath<T>(T obj, string path)
        {
            var retry = 3;
            while (retry > 0)
            {
                try
                {
                    var output = JsonSerializer.Serialize(obj, GetSettings());
                    _ = FileSystemWrapper.WriteAllText(path, output);
                    break;
                }
                catch (Exception)
                {
                }
                retry--;
            }
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
                var encoderSettings = new TextEncoderSettings();
                encoderSettings.AllowCharacters('\u0026');
                encoderSettings.AllowCharacters('&');
                encoderSettings.AllowRange(UnicodeRanges.All);
                settings = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(encoderSettings),
                    WriteIndented = true,
                    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                    UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode
                };
            }
            return settings;
        }

        public static async Task<string> ReadAllText(string path)
        {
            if (string.IsNullOrEmpty(path) || !await FileSystemWrapper.Exists(path))
            {
                return "";
            }
            var retry = 3;

            while (retry > 0)
            {
                try
                {
                    /*using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using var textReader = new StreamReader(fileStream);
                    var content = textReader.ReadToEnd();*/

                    return await FileSystemWrapper.Instance.ReadAllTextAsync(path);
                }
                catch (Exception)
                {
                }
                retry--;
            }
            return "";
        }

        public static object ToExactType<T>(JsonObject obj)
        {
            return DeserializeString<T>(obj.ToJsonString());
        }
    }
}