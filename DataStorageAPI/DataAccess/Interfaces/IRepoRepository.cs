using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.DataAccess.Interfaces
{
    /// <summary>
    /// Defines repository for Repository objects.
    /// </summary>
    public interface IRepoRepository
    {
        /// <summary>
        /// Gets all repository entities.
        /// </summary>
        /// <returns>List of repository entities.</returns>
        Task<IEnumerable<RepositoryEntity>> GetRepositoryEntitiesAsync();

        /// <summary>
        /// Gets a repository entity by id.
        /// </summary>
        /// <param name="repositoryId">Id of the repository.</param>
        /// <exception cref="NotFoundException">Thrown when the repository is not found.</exception>
        /// <returns>Repository entity.</returns>
        Task<RepositoryEntity> GetRepositoryEntityAsync(string repositoryId);

        /// <summary>
        /// Creates a new repository entity.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="repositoryEntity">Repository entity object.</param>
        /// <returns>Created repository entity.</returns>
        Task<RepositoryEntity> CreateRepositoryAsync(string repositoryId, RepositoryEntity repositoryEntity);

        /// <summary>
        /// Deletes a given repository by id
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <returns>Boolean.</returns>
        Task<bool> DeleteRepositoryAsync(string repositoryId);

        /// <summary>
        /// Updates a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="repositoryEntity">Repository update.</param>
        /// <returns>Updated repository entity.</returns>
        Task<RepositoryEntity> UpdateRepositoryAsync(string repositoryId, RepositoryEntity repositoryEntity);
    }
}
