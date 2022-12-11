namespace DataStorageAPI.ServiceLayer.Models
{
    /// <summary>
    /// Model to tell whether an object is created for first time or updated.
    /// </summary>
    public class CreateOrUpdateReponse<T>
    {
        public T Value { get; set; }
        public bool IsCreated { get; set; }
    }
}
