using DataStorageAPI.DataAccess.Interfaces;
using DataStorageAPI.DataStore;
using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.DataAccess
{
    public class RepoRepository: IRepoRepository
    {
        private readonly IDataStore dataStore;
        public RepoRepository(IDataStore dataStore) 
        {
            this.dataStore = dataStore;
        }

        /// <summary>
        /// Creates a new repository.
        /// </summary>
        /// <param name="repositoryEntity">Repository entity.</param>
        /// <returns>Created repository entity.</returns>
        public async Task<RepositoryEntity> CreateRepositoryAsync(string repositoryId, RepositoryEntity repositoryEntity)
        {
            return await this.dataStore.CreateRepository(repositoryId, repositoryEntity).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns>Boolean, true if the repository is deleted, false otherwise.</returns>
        public async Task<bool> DeleteRepositoryAsync(string repositoryId)
        {
            return await this.dataStore.DeleteRepository(repositoryId).ConfigureAwait(false); ;
        }

        /// <summary>
        /// Gets all repositories.
        /// </summary>
        /// <returns>List of repositories.</returns>
        public async Task<IEnumerable<RepositoryEntity>> GetRepositoryEntitiesAsync()
        {
            return await this.dataStore.GetAllRepositories().ConfigureAwait(false); ;
        }

        /// <summary>
        /// Gets a repository with given id.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>Repository</returns>
        public async Task<RepositoryEntity> GetRepositoryEntityAsync(string repositoryId)
        {
            return await this.dataStore.GetRepositoryById(repositoryId).ConfigureAwait(false); ;
        }

        public async Task<RepositoryEntity> UpdateRepositoryAsync(string repositoryId, RepositoryEntity repositoryEntity)
        {
            return await this.dataStore.UpdateRepository(repositoryId, repositoryEntity).ConfigureAwait(false); ;
        }
    }
}
