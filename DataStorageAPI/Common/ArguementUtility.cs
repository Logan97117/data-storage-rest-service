namespace DataStorageAPI.Common
{
    /// <summary>
    /// A utility used to check for null arguements.
    /// </summary>
    public static partial class ArguementUtility
    {
        /// <summary>
        /// Checks if an object is null.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="value">Object value.</param>
        /// <param name="varName">Variable name.</param>
        /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
        public static void CheckForNull<T>(T value, string varName)
        {
            if (value == null) 
            {
                throw new ArgumentNullException(string.Format(CommonConstants.ArgumentNullExceptionMessage, varName));
            }
        }

        /// <summary>
        /// Checks for string null or empty or whitespace.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <param name="varName">Variable name.</param>
        /// <exception cref="ArgumentException">Thrown when the string is null or whitespaced or empty.</exception>
        public static void CheckForStringNullOrEmptyOrWhiteSpace(string value, string varName) 
        {
            CheckForNull(value, varName);
            if (value.Length == 0 || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(string.Format(CommonConstants.NotFoundExceptionMessage, varName));  
            }
        }
    }
}
