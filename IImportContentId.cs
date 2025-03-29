using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PluginBase.IPluginBase;

namespace PluginBase
{
    /// <summary>
    /// If your plugin can work on content id's (polling id) of other plugins, inherit from this class
    /// This is usefull when you have two plugin, one creates a video in some service and other one upscales them
    /// </summary>
    public interface IImportContentId
    {
        /// <summary>
        /// Return true if plugin can work with contnt id from plugin from track (image or video)
        /// </summary>
        bool CanImportFrom(string pluginUniqueName, TrackType track);

        /// <summary>
        /// Create new item payload from content id
        /// </summary>
        object ItemPayloadFromContentId(string id);
    }
}