﻿namespace PluginBase
{
    public static class CommonConstants
    {
        public const string DefaultIntToFrameFormat = "%09d";
        public const string DefaultIntToStringFormat = "D9";

        public static List<string> AllTypes => [.. ImgTypes, .. VideoTypes, .. AudioTypes, .. LyricTypes];
        public static List<string> ImgTypes => [".png", ".jpg", ".jpeg", ".bmp", ".tga", ".webp"];
        public static List<string> VideoTypes => [".avi", ".mp4", ".divx", ".wmv", ".mts", ".mpg", ".mkv"];
        public static List<string> AudioTypes => [".wav", ".wma", ".mp3", ".ogg", ".rma", ".m4a"];
        public static List<string> LyricTypes => [".txt", ".lrc"];
    }
}