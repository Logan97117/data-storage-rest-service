using DataStorageAPI.DataAccess.Interfaces;
using DataStorageAPI.Exceptions;
using DataStorageAPI.ServiceLayer.Interfaces;
using DataStorageAPI.ServiceLayer.Models;

namespace DataStorageAPI.ServiceLayer
{
    public class RepositoryService: IRepositoryService
    {
        private readonly IRepoRepository repoRepository;
        public RepositoryService(IRepoRepository repoRepository)
        {
            this.repoRepository = repoRepository;
        }

        /// <summary>
        /// Creates or updates a repository.
        /// </summary>
        /// <param name="repositoryId">RepositoryId</param>
        /// <param name="repository">Repository</param>
        /// <returns>CreateOrUpdateResponse for Repository.</returns>
        public async Task<CreateOrUpdateReponse<RepositoryEntity>> CreateOrUpdateRepositoryAsync(string repositoryId, RepositoryEntity repository)
        {
            CreateOrUpdateReponse<RepositoryEntity> response = new CreateOrUpdateReponse<RepositoryEntity>();
            bool shouldCreate = false;

            try
            {
                RepositoryEntity repositoryEntity = await this.GetRepositoryAsync(repositoryId).ConfigureAwait(false);
            }
            catch (NotFoundException ex)
            {
                shouldCreate = true;
            }

            if (shouldCreate) 
            {
                RepositoryEntity newRepository = await this.repoRepository.CreateRepositoryAsync(repositoryId, repository).ConfigureAwait(false);
                response.IsCreated = true;
                response.Value = newRepository;
                return response;
            }

            response.IsCreated = false;
            response.Value = await this.repoRepository.UpdateRepositoryAsync(repositoryId, repository).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Deletes a repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns>Repository.</returns>
        public async Task<RepositoryEntity> DeleteRepositoryAsync(string repositoryId)
        {
            try
            {
                RepositoryEntity repositoryEntity = await this.GetRepositoryAsync(repositoryId).ConfigureAwait(false);
                await this.repoRepository.DeleteRepositoryAsync(repositoryId).ConfigureAwait(false);
                return repositoryEntity;
            }
            catch (NotFoundException ex)
            {
                //Logger
                return null;
            }
        }

        /// <summary>
        /// Returns all repositories.
        /// </summary>
        /// <returns>List of Repositories.</returns>
        public async Task<IEnumerable<RepositoryEntity>> GetAllRepositoriesAsync()
        {
            return await this.repoRepository.GetRepositoryEntitiesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a repository with given id.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the given repository does not exists.</exception>
        /// <returns>Repository.</returns>
        public async Task<RepositoryEntity> GetRepositoryAsync(string repositoryId)
        {
            return await this.repoRepository.GetRepositoryEntityAsync(repositoryId).ConfigureAwait(false);
        }
    }
}
