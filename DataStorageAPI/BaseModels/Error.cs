using System.Net;

namespace DataStorageAPI.BaseModels
{
    /// <summary>
    /// A model class for serializing errors.
    /// </summary>
    public class Error
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
