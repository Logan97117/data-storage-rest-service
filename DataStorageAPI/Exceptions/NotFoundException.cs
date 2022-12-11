namespace DataStorageAPI.Exceptions
{
    /// <summary>
    /// Exception thrown when a resource does not exists.
    /// </summary>
    public class NotFoundException: Exception
    {
        public NotFoundException(string message): base(message) 
        {

        }
    }
}
