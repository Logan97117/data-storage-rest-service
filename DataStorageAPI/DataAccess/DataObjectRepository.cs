using DataStorageAPI.DataAccess.Interfaces;
using DataStorageAPI.DataStore;
using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.DataAccess
{
    public class DataObjectRepository : IDataObjectRepository
    {
        private readonly IDataStore dataStore;
        public DataObjectRepository(IDataStore dataStore) 
        {
            this.dataStore = dataStore;
        }

        /// <summary>
        /// Creates a data object under repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="dataObjectEntity">Data object entity.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>Created data object.</returns>
        public async Task<DataObjectEntity> CreateDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId, DataObjectEntity dataObjectEntity)
        {
            return await this.dataStore.CreateDataObjectUnderRepository(dataObjectEntity, repositoryId, dataObjectId).ConfigureAwait(false); ;
        }

        /// <summary>
        /// Deletes a data object under repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>Boolean whether the object was deleted successfully.</returns>
        public async Task<bool> DeleteDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId)
        {
            return await this.dataStore.DeleteDataObjectUnderRepository(dataObjectId, repositoryId).ConfigureAwait(false); ;
        }

        /// <summary>
        /// Returns all data objects under a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>List of data objects.</returns>
        public async Task<IEnumerable<DataObjectEntity>> GetAllDataObjectsUnderRepositoryAsync(string repositoryId)
        {
            return await this.dataStore.GetAllDataObjectsUnderRepository(repositoryId).ConfigureAwait(false); ;
        }

        /// <summary>
        /// Gets a data object under repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">When the repository or the data object does not exists.</exception>
        /// <returns></returns>
        public async Task<DataObjectEntity> GetDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId)
        {
            return await this.dataStore.GetDataObjectUnderRepository(dataObjectId, repositoryId).ConfigureAwait(false); ;
        }

        /// <summary>
        /// Updates the data object under repository.
        /// </summary>
        /// <param name="dataObjectUpdate">Data object update.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <returns></returns>
        public async Task<DataObjectEntity> UpdateDataObjectUnderRepositoryAsync(DataObjectEntity dataObjectUpdate, string dataObjectId)
        {
            return await this.dataStore.UpdateDataObjectUnderRepository(dataObjectUpdate, dataObjectId).ConfigureAwait(false); ;
        }
    }
}
