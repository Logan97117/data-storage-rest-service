namespace DataStorageAPI.Exceptions
{
    /// <summary>
    /// An exception class for handling null request body.
    /// </summary>
    public class ArguementNullException: Exception
    {
        public ArguementNullException(string message) : base(message) { }
    }
}
