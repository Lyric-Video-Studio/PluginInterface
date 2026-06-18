using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    /// <summary>
    /// If you want your plugin to be connected to MCP server, inherit ConnectionSettings (or similar)-classs from this
    /// Only plugins that have this intereface in the settings are qualified for MCP usage. User has option to toggle plugin acces on/off
    /// In general, if plugin generations cost money (API), default access to false. Local plugins are usually good for defualting true
    /// </summary>
    public interface IAllowMcpGeneration
    {
        bool AllowMcpAccess { get; set; }
    }
}
