using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    public interface ITrackPayloadFromModel
    {
        /// <summary>
        /// Create new track payload based on model.
        /// </summary>
        object TrackPayloadFromModel(string model);
    }
    public class SupportedPluginModel
    {
        public IPluginBase.TrackType TrackType { get; set; }
        public string Category { get; set; } = "";
        public string Model { get; set; } = "";
        public string DisplayName { get; set; } = "";
    }

    public interface IPluginSupportedModels
    {
        /// <summary>
        /// Return exact model preset strings that can be passed to ITrackPayloadFromModel.
        /// </summary>
        IReadOnlyList<SupportedPluginModel> GetSupportedModels();
    }
}
