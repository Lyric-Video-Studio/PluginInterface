using System.Net;

namespace PluginBase
{
    /// <summary>
    /// You can request app to upload content to user chosen CDN. You can call this interface without checking if the file is already uploaded,
    /// the interface will return immediately if so. File should always be the local shource file, let interface implementation do it's stuff. Note that uploading might fail, so catch the http status code
    /// User might have not activated any content delivery methods, in that case interface will return true is 'is local file' and you must handle error message for the user
    /// Preferred error message is: 'Image needs to be publicly availabe link or you must enable content delivery from Settings'
    /// It is recommended that in your payload, only show the local source file for user and not store the url given from interface, since it might be temporary link
    /// </summary>
    public interface IContentUploader
    {
        Task<(HttpStatusCode responseCode, string uploadedUrl, bool isLocalFile)> RequestContentUpload(string localFilePath);
    }
}