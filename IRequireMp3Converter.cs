using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    /// <summary>
    /// Inherit this if your plugin requires that audio should be uploaded at 160kpb mp3
    /// </summary>
    public interface IRequireMp3Converter
    {
        public void SetMp3Converter(IMp3Converter converter);
    }

    /// <summary>
    /// Converts audio to 160kpb mp3. Let me know if you need more conversion params
    /// </summary>
    public interface IMp3Converter
    {
        public string ConvertToMp3(string path);
    }
}
