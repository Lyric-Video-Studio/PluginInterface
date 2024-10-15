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

    public class IgnoreDynamicEdit : Attribute
    {
    }

    /// <summary>
    /// This attribute makes the property visible even if you define it as JsonIgnore. Purpose of this is to provide user some easier value for edit, 
    /// where the setter actually modifies something else
    /// </summary>
    public class ForceShowOnDynamicEdit: Attribute
    {

    }
}