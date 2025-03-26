using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginBase
{
    public interface IImportFromVideo
    {
        /// <summary>
        /// Create new item payload based on video source.
        /// </summary>
        public object ItemPayloadFromVideoSource(string videoSource);
    }
}