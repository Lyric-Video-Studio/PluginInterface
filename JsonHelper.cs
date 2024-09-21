using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PluginBase
{
    public static class JsonHelper
    {
        public static List<string> Errors = new List<string>();

        public static T DeserializeString<T>(string obj)
        {
            var output = JsonConvert.DeserializeObject<T>(obj, GetSettings());
            return output;
        }

        public static T Deserialize<T>(string path)
        {
            return DeserializeString<T>(File.ReadAllText(path));
        }

        public static string Serialize<T>(T obj)
        {
            var output = JsonConvert.SerializeObject(obj, Formatting.Indented, GetSettings());
            return output;
        }

        public static void SerializeToPath<T>(T obj, string path)
        {
            var output = JsonConvert.SerializeObject(obj, Formatting.Indented, GetSettings());
            File.WriteAllText(path, output);
        }

        public static T DeepCopy<T>(object obj)
        {
            return DeserializeString<T>(Serialize(obj));
        }

        private static JsonSerializerSettings settings;

        private static JsonSerializerSettings GetSettings()
        {
            if (settings == null)
            {
                settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, NullValueHandling = NullValueHandling.Ignore };

                settings.Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    Errors.Add(args.ErrorContext.Error.ToString());
                    args.ErrorContext.Handled = true;
                };
            }
            return settings;
        }
    }
}