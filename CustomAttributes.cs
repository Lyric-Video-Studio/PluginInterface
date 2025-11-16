namespace PluginBase
{
    public class EditorMaxHeigthAttribute : Attribute
    {
        public readonly int Heigth = -1;

        public EditorMaxHeigthAttribute()
        {
        }

        public EditorMaxHeigthAttribute(int heigth)
        {
            Heigth = heigth;
        }
    }

    public class EditorWidthAttribute : Attribute
    {
        public readonly int Width = 200;

        public EditorWidthAttribute()
        {
        }

        public EditorWidthAttribute(int width)
        {
            Width = width;
        }
    }

    public class EditorColumnSpanAttribute : Attribute
    {
        public readonly int ColumsSpan = 0;

        public EditorColumnSpanAttribute()
        {
        }

        public EditorColumnSpanAttribute(int columsSpan)
        {
            ColumsSpan = columsSpan;
        }
    }

    public class ParentNameAttribute : Attribute
    {
        public readonly string Name = "";

        public ParentNameAttribute()
        {
        }

        public ParentNameAttribute(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Define custom name for property, if you do not wish to use property name
    /// Usefull in cases where you have properties that mean the same but are not visible at the same time
    public class CustomNameAttribute : Attribute
    {
        public readonly string Name = "";

        public CustomNameAttribute()
        {
        }

        public CustomNameAttribute(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// By default, nested properties are names "ParentPropertyName - Property name", use this if you do not wish to show then
    /// For example, if your payload is just a "wrapper" for another class and/or the names can't be confused
    /// </summary>
    public class IgnorePropertyName : Attribute
    {
    }

    /// <summary>
    /// Hides this property from view
    /// </summary>
    public class IgnoreDynamicEdit : Attribute
    {
    }

    /// <summary>
    /// This attribute makes the property visible even if you define it as JsonIgnore. Purpose of this is to provide user some easier value for edit,
    /// where the setter actually modifies something else
    /// </summary>
    public class ForceShowOnDynamicEdit : Attribute
    {
    }

    /// <summary>
    /// Add button to perform custom action. Attach this to public function, no parameters, Function name must follow CamelCase-Style, to better user exp.
    /// Dynamic view is refreshed after invoking the function
    /// </summary>
    public class CustomAction(string frienlyName) : Attribute
    {
        public string FriendlyName { get; } = frienlyName;
    }

    /// <summary>
    /// Allow dropping of files to this property. Only supported for strings
    /// </summary>
    public class EnableFileDrop : Attribute
    {
    }

    /// <summary>
    /// Enable folder picker. When editor field gets focus, it will first launch folder pick dialog and only after that, allows user to manually edit it
    /// </summary>
    public class EnableFolderPicker : Attribute
    {
    }

    /// <summary>
    /// Mask the letters of text box with *, for api keys etc
    /// </summary>
    public class MaskInput : Attribute
    {
    }
}