namespace DataStorageAPI.ApiLayer
{
    /// <summary>
    /// Contains constants for route names for controllers.
    /// </summary>
    public static partial class Routenames
    {
        /// <summary>
        /// Routename for api/repository/{repositoryId}
        /// </summary>
        public const string RepoById = "getRepoById";

        /// <summary>
        /// Routename for api/repository/{repositoryId}/dataobjects/{dataObjectId}
        /// </summary>
        public const string DataObjectById = "getDataObjectById";

    }
}
