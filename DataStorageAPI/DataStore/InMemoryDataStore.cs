using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Common;
using DataStorageAPI.Exceptions;
using System.Collections.Concurrent;

namespace DataStorageAPI.DataStore
{
    /// <summary>
    /// In memory data store
    /// </summary>
    public class InMemoryDataStore : IDataStore
    {
        private ConcurrentDictionary<string, RepositoryEntity> repositories;
        private ConcurrentDictionary<string, DataObjectEntity> dataObjects;
        private ConcurrentDictionary<string, List<string>> repositoriesVsDataObjectIds;
        public InMemoryDataStore()
        {
            this.repositories = new ConcurrentDictionary<string, RepositoryEntity>();
            this.dataObjects = new ConcurrentDictionary<string, DataObjectEntity>();
            this.repositoriesVsDataObjectIds = new ConcurrentDictionary<string, List<string>>();
        }

        /// <summary>
        /// Gets all repositories.
        /// </summary>
        /// <returns>List of repositories.</returns>
        public Task<IEnumerable<RepositoryEntity>> GetAllRepositories()
        {
            return Task.FromResult(this.repositories.Values.Cast<RepositoryEntity>());
        }

        /// <summary>
        /// Returns a repository by ID.
        /// </summary>
        /// <param name="repositoryId">Repository Id.</param>
        /// <returns>Repository entity</returns>
        /// <exception cref="NotFoundException"></exception>
        public Task<RepositoryEntity> GetRepositoryById(string repositoryId)
        {
            bool repositoryFound = this.repositories.TryGetValue(repositoryId, out var repository);

            if (!repositoryFound)
            {
                throw new NotFoundException(string.Format(CommonConstants.NotFoundExceptionMessage, "Repository", repositoryId));
            }

            return Task.FromResult(repository);
        }

        /// <summary>
        /// Creates a new repository.
        /// </summary>
        /// <param name="repository">Repository entity.</param>
        /// <returns>Repository entity or null if already exists.</returns>
        public Task<RepositoryEntity> CreateRepository(string repositoryId, RepositoryEntity repository)
        {
            string Id = repositoryId != null? repositoryId: Guid.NewGuid().ToString();
            RepositoryEntity newRepository = new RepositoryEntity()
            {
                Id = Id,
                Name = repository.Name,
                Metadata = repository.Metadata,
            };

            this.repositories.TryAdd(repositoryId, newRepository);
            this.repositoriesVsDataObjectIds.TryAdd(repositoryId, new List<string>());
            return Task.FromResult(newRepository);
        }

        /// <summary>
        /// Deletes a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository Id.</param>
        /// <returns>Boolean.</returns>
        public Task<bool> DeleteRepository(string repositoryId)
        {
            bool repositoryRemoved = this.repositories.Remove(repositoryId, out var existingRepository);
            return Task.FromResult(repositoryRemoved);
        }

        /// <summary>
        /// Gets all data objects under a given repository.
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <returns>List of data objects.</returns>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        public Task<IEnumerable<DataObjectEntity>> GetAllDataObjectsUnderRepository(string repositoryId)
        {
            bool repositoryFound = this.repositories.TryGetValue(repositoryId, out var repository);

            if (!repositoryFound)
            {
                throw new NotFoundException(string.Format(CommonConstants.NotFoundExceptionMessage, "Repository", repositoryId));
            }

            this.repositoriesVsDataObjectIds.TryGetValue(repositoryId, out var dataObjectIds);

            return Task.FromResult(dataObjectIds.Select<string, DataObjectEntity>((id) => 
            {
                this.dataObjects.TryGetValue(id, out var dataObject);
                return dataObject;
            }));           
        }

        /// <summary>
        /// Gets a data object under a given repository.
        /// </summary>
        /// <param name="dataObjectId"></param>
        /// <param name="repositoryId"></param>
        /// <returns>Data object.</returns>
        /// <exception cref="NotFoundException">Thrown when either repository or data object does not exist.</exception>
        public Task<DataObjectEntity> GetDataObjectUnderRepository(string dataObjectId, string repositoryId)
        {
            bool repositoryFound = this.repositories.TryGetValue(repositoryId, out var repository);

            if (!repositoryFound)
            {
                throw new NotFoundException(string.Format(CommonConstants.NotFoundExceptionMessage, "Repository", repositoryId));
            }

            this.repositoriesVsDataObjectIds.TryGetValue((repositoryId), out var dataObjectIds);
            bool dataObjectFound = this.dataObjects.TryGetValue(dataObjectId, out var dataObject);
            bool dataObjectFoundUnderRepository = dataObjectId.Contains(dataObjectId);

            if (!dataObjectFound || !dataObjectFoundUnderRepository)
            {
                throw new NotFoundException(string.Format(CommonConstants.NotFoundExceptionMessage, "DataObject", dataObjectId));
            }

            return Task.FromResult(dataObject);
        }

        /// <summary>
        /// Creates a new data object under repository.
        /// </summary>
        /// <param name="dataObject">Data object.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns>Created data object.</returns>
        /// <exception cref="NotFoundException">Thrown when repository does not exists.</exception>
        public Task<DataObjectEntity> CreateDataObjectUnderRepository(DataObjectEntity dataObject, string repositoryId, string dataObjectId)
        {
            bool repositoryFound = this.repositories.TryGetValue(repositoryId, out var repository);

            if (!repositoryFound)
            {
                throw new NotFoundException(string.Format(CommonConstants.NotFoundExceptionMessage, "Repository", repositoryId));
            }

            this.repositoriesVsDataObjectIds.TryGetValue(repositoryId, out var dataObjectIds);
            string Id = dataObjectId != null? dataObjectId: Guid.NewGuid().ToString();
            DataObjectEntity newDataObjectEntity = new DataObjectEntity()
            {
                Id = Id,
                Name = dataObject.Name,
                Size = dataObject.Size,
            };

            this.dataObjects.TryAdd(dataObjectId, newDataObjectEntity);
            dataObjectIds.Add(dataObjectId);
            return Task.FromResult<DataObjectEntity>(newDataObjectEntity);
        }

        /// <summary>
        /// Deletes a data object from a given repository
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public Task<bool> DeleteDataObjectUnderRepository(string dataObjectId, string repositoryId)
        {
            bool repositoryFound = this.repositories.TryGetValue(repositoryId, out var repository);

            if (!repositoryFound)
            {
                throw new NotFoundException(string.Format(CommonConstants.NotFoundExceptionMessage, "Repository", repositoryId));
            }

            this.repositoriesVsDataObjectIds.TryGetValue((repositoryId), out var dataObjectIds);
            bool dataObjectRemovedUnderRepository = dataObjectIds.Remove(dataObjectId);
            bool dataObjectRemoved = this.dataObjects.Remove(dataObjectId, out var dataObject);
            return Task.FromResult(dataObjectRemovedUnderRepository && dataObjectRemoved);
        }

        /// <summary>
        /// Updates a repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="repositoryUpdate">Repository update.</param>
        /// <returns>Updated repository.</returns>
        public Task<RepositoryEntity> UpdateRepository(string repositoryId, RepositoryEntity repositoryUpdate)
        {
            this.repositories.Remove(repositoryId, out var repository);
            repository.Name = repositoryUpdate.Name;
            repository.Metadata = repositoryUpdate.Metadata;
            this.repositories.TryAdd(repositoryId, repository);
            return Task.FromResult(repository);
        }

        /// <summary>
        /// Updates a data object.
        /// </summary>
        /// <param name="dataObjectUpdate">Data object update.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <returns></returns>
        public Task<DataObjectEntity> UpdateDataObjectUnderRepository(DataObjectEntity dataObjectUpdate, string dataObjectId)
        {
            this.dataObjects.Remove(dataObjectId, out var dataObject);
            dataObject.Name = dataObjectUpdate.Name;
            dataObject.Size = dataObjectUpdate.Size;
            this.dataObjects.TryAdd(dataObjectId, dataObject);
            return Task.FromResult(dataObject);
        }
    }
}
