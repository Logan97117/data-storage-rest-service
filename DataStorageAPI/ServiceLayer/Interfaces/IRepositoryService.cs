using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.ServiceLayer.Interfaces
{
    /// <summary>
    /// Interface for repository service.
    /// </summary>
    public interface IRepositoryService
    {
        /// <summary>
        /// Gets all repositories.
        /// </summary>
        /// <returns>List of repositories.</returns>
        Task<IEnumerable<RepositoryEntity>> GetAllRepositoriesAsync();

        /// <summary>
        /// Creates a new repository or updates an existing repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="repository">Repository update.</param>
        /// <returns>CreateOrUpdate response for repository.</returns>
        Task<CreateOrUpdateReponse<RepositoryEntity>> CreateOrUpdateRepositoryAsync(string repositoryId, RepositoryEntity repository);

        /// <summary>
        /// Deletes a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns>Deleted repository.</returns>
        Task<RepositoryEntity> DeleteRepositoryAsync(string repositoryId);

        /// <summary>
        /// Gets a repository with given ID.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>Repository entity.</returns>
        Task<RepositoryEntity> GetRepositoryAsync(string repositoryId);
    }
}
