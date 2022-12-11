using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.DataStore
{
    /// <summary>
    /// Interface for the in memory data store implementations.
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Gets all repositories.
        /// </summary>
        /// <returns>All repositories.</returns>
        Task<IEnumerable<RepositoryEntity>> GetAllRepositories();
        
        /// <summary>
        /// Gets a repository by id.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when repository with given ID is not found.</exception>
        /// <returns>Repository.</returns>
        Task<RepositoryEntity> GetRepositoryById(string repositoryId);

        /// <summary>
        /// Creates a new repository entity
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns>Created repository entity</returns>
        Task<RepositoryEntity> CreateRepository(string repositoryId, RepositoryEntity repository);

        /// <summary>
        /// Updates a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="repositoryUpdate">Repository update.</param>
        /// <returns></returns>
        Task<RepositoryEntity> UpdateRepository(string repositoryId, RepositoryEntity repositoryUpdate);
        
        /// <summary>
        /// Deletes a repository
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <returns>Boolean.</returns>
        Task<bool> DeleteRepository(string repositoryId);

        /// <summary>
        /// Get all data objects under repository
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns>List of data objects.</returns>
        Task<IEnumerable<DataObjectEntity>> GetAllDataObjectsUnderRepository(string repositoryId);

        /// <summary>
        /// Get a data object under repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the data object with given ID is not found under repository.</exception>
        /// <returns>Data object</returns>
        Task<DataObjectEntity> GetDataObjectUnderRepository(string dataObjectId, string repositoryId);

        /// <summary>
        /// Create a new data object under repository.
        /// </summary>
        /// <param name="dataObject">Data object.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <returns>Created data object</returns>
        Task<DataObjectEntity> CreateDataObjectUnderRepository(DataObjectEntity dataObject, string repositoryId, string dataObjectId);

        /// <summary>
        /// Deletes a data object under repository
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns>boolean.</returns>
        Task<bool> DeleteDataObjectUnderRepository(string dataObjectId, string repositoryId);

        /// <summary>
        /// Updates a given data object under given repository.
        /// </summary>
        /// <param name="dataObjectUpdate">Data object update.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <returns></returns>
        Task<DataObjectEntity> UpdateDataObjectUnderRepository(DataObjectEntity dataObjectUpdate, string dataObjectId);
    }
}
