namespace DataStorageAPI.Common
{
    /// <summary>
    /// A class for constants
    /// </summary>
    public static partial class CommonConstants
    {
        /// <summary>
        /// Max ID length
        /// </summary>
        public const int MaxIdLength = 50;

        /// <summary>
        /// Min ID length
        /// </summary>
        public const int MinIdLength = 2;

        /// <summary>
        /// String format for not found exception message.
        /// </summary>
        public const string NotFoundExceptionMessage = "Resource {0} with id {1} does not exists.";

        /// <summary>
        /// String format for argument null exception message.
        /// </summary>
        public const string ArgumentNullExceptionMessage = "{0} cannot be null.";

        /// <summary>
        /// String format for argument exception message.
        /// </summary>
        public const string ArgumentExceptionMessage = "{0} cannot be null or empty.";
    }
}
