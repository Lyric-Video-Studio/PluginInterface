namespace PluginBase
{
    public interface IPropertyOptionsProvider
    {
        /// <summary>
        /// Return selection options if property should be shown as combobox / selection. Return null or empty list if no selection options for property
        /// </summary>
        public Task<string[]> SelectionOptionsForProperty(string propertyName);
    }
}