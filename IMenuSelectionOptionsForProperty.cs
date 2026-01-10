using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    public interface IMenuSelectionOptionsForProperty
    {
        /// <summary>
        /// Return selection options if property should be shown as menu tree. If you plugin inherits this for property, it will be used instead of IPropertyOptionsProvider FOR THAT SPECIFIC property
        /// return empty dictionary if property does not wish to be displayed as menu / tree structure
        /// </summary>
        public Task<Dictionary<string, string[]>> MenuSelectionOptionsForProperty(string propertyName);

    }
}
