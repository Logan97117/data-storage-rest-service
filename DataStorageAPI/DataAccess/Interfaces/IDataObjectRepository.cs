using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.DataAccess.Interfaces
{
    /// <summary>
    /// Defines data object repository.
    /// </summary>
    public interface IDataObjectRepository
    {
        /// <summary>
        /// Gets all data objects under a given repository.
        /// </summary>
        /// <param name="repositoryid">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository with given id does not exist.</exception>
        /// <returns>List of data objects.</returns>
        Task<IEnumerable<DataObjectEntity>> GetAllDataObjectsUnderRepositoryAsync(string repositoryid);

        /// <summary>
        /// Gets a data object under a given repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when either the repository or the data object does not exist.</exception>
        /// <returns></returns>
        Task<DataObjectEntity> GetDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId);

        /// <summary>
        /// Creates a data object under repository.
        /// </summary>
        /// <param name="dataObjectEntity">Data object.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exist.</exception>
        /// <returns>Created data object entity.</returns>
        Task<DataObjectEntity> CreateDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId, DataObjectEntity dataObjectEntity);

        /// <summary>
        /// Updates a data object under repository.
        /// </summary>
        /// <param name="dataObjectUpdate">Data object update.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <returns></returns>
        Task<DataObjectEntity> UpdateDataObjectUnderRepositoryAsync(DataObjectEntity dataObjectUpdate, string dataObjectId);

        /// <summary>
        /// Deletes a data object under a given repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown if the repository does not exists.</exception>
        /// <returns></returns>
        Task<bool> DeleteDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId);
    }
}
