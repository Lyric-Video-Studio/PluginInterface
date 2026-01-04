using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    public interface ITrackPayloadFromModel
    {
        /// <summary>
        /// Create new track payload based on text.
        /// </summary>
        object TrackPayloadFromModel(string model);
    }
}
