using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.ServiceLayer.Interfaces
{
    /// <summary>
    /// Inteface for data object service.
    /// </summary>
    public interface IDataObjectService
    {
        /// <summary>
        /// Gets data objects under a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository Id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>List of data objects.</returns>
        Task<IEnumerable<DataObjectEntity>> GetDataObjectsUnderRepositoryAsync(string repositoryId);

        /// <summary>
        /// Gets a data object under repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the data object or repository does not exists.</exception>
        /// <returns></returns>
        Task<DataObjectEntity> GetDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId);

        /// <summary>
        /// Creates or updates a data object under a given repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="dataObject">Data object id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>CreateOrUpdate response for data object entity.</returns>
        Task<CreateOrUpdateReponse<DataObjectEntity>> CreateOrUpdateUnderRepositoryAsync(string dataObjectId, string repositoryId, DataObjectEntity dataObject);

        /// <summary>
        /// Deletes a data object under a repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <returns>Deleted data object.</returns>
        Task<DataObjectEntity> DeleteUnderRepositoryAsync(string repositoryId, string dataObjectId);
    }
}
