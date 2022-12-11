namespace DataStorageAPI.Exceptions
{
    /// <summary>
    /// An exception class to handle null or zero length arguements.
    /// </summary>
    public class ArguementException: Exception
    {
        public ArguementException(string message) : base(message) { }
    }
}
