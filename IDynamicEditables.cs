using System;
using System.Collections.Generic;
using System.Text;

namespace PluginBase
{
    public interface IDynamicEditables
    {
        List<DynamicEditableField> GetEditables();
        void SetValue(string key, object? value);
        object? GetValue(string key);        
    }

    public class DynamicEditableField
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public object? Value { get; set; }

        public Type ValueType { get; set; } = typeof(string);

        public string Description { get; set; } = "";
        public string[] Options { get; set; } = [];
        public bool Multiline { get; set; }
        public bool EnableFileDrop { get; set; }
        public bool EnableFolderPicker { get; set; }
        public bool MaskInput { get; set; }
        public int Width { get; set; } = 200;
        public int MaxHeight { get; set; } = 200;
        public int ColumnSpan { get; set; } = 1;
        public bool Visible { get; set; } = true;
    }
}
