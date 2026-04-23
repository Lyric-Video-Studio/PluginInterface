using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    // Use this intefrace to launch browser
    public interface IUriLauncher
    {
        public static IUriLauncher Launcher { get; set; }

        void LaunchUrl(string url);
    }
}
