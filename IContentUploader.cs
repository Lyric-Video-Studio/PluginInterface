using System.Net;

namespace PluginBase
{
    /// <summary>
    /// You can request app to upload content to user chosen CDN. You can call this interface withotu checking if the file is already uploaded,
    /// the interface will return immediately if so. If file is local file, it is uploaded. Note that uploading might fails, so catch the http status code
    /// User might have not activated any content delivery methods, in that case interface will return true is 'is local file' and you must handle error message for the user
    /// Preferred error message is: 'Image needs to be publicly availabe link or you must enable content delivert from Settings'
    /// </summary>
    public interface IContentUploader
    {
        Task<(HttpStatusCode responseCode, string uploadedUrl, bool isLocalFile)> RequestContentUpload(string url);
    }
}