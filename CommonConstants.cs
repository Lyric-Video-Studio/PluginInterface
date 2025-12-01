namespace PluginBase
{
    public static class CommonConstants
    {
        public const string DefaultIntToFrameFormat = "%09d";
        public const string DefaultIntToStringFormat = "D9";

        public static List<string> AllTypes => [.. ImgTypes, .. VideoTypes, .. AudioTypes, .. LyricTypes];
        public static List<string> ImgTypes => [".png", ".jpg", ".jpeg", ".bmp", ".tga", ".webp"];
        public static List<string> VideoTypes => [".avi", ".mp4", ".divx", ".wmv", ".mts", ".mpg", ".mkv"];
        public static List<string> AudioTypes => [".wav", ".wma", ".mp3", ".ogg", ".rma", ".m4a"];
        public static List<string> LyricTypes => [".txt", ".lrc", ".vtt", ".srt", ".tsv"];

        public static string GetMimeType(string extension)

        {
            return extension.ToLower() switch

            {
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".flac" => "audio/flac",
                ".m4a" => "audio/x-m4a",
                ".aac" => "audio/aac",
                ".ogg" => "audio/ogg",
                ".opus" => "audio/opus",
                ".webm" => "audio/webm",
                ".mp4" => "video/mp4",
                ".avi" => "video/x-msvideo",
                ".mkv" => "video/x-matroska",
                ".mov" => "video/quicktime",
                ".wmv" => "video/x-ms-wmv",
                ".flv" => "video/x-flv",
                ".3gp" => "video/3gpp",

                _ => "audio/mpeg" // Default fallback
            };
        }
    }
}