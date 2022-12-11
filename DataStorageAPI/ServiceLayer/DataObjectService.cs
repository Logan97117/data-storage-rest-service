using DataStorageAPI.DataAccess.Interfaces;
using DataStorageAPI.ServiceLayer.Interfaces;
using DataStorageAPI.ServiceLayer.Models;
using DataStorageAPI.Exceptions;

namespace DataStorageAPI.ServiceLayer
{
    public class DataObjectService: IDataObjectService
    {
        private readonly IDataObjectRepository dataObjectRepository;
        private readonly IRepositoryService repositoryService;
        public DataObjectService(
            IDataObjectRepository dataObjectRepository,
            IRepositoryService repositoryService) 
        {
            this.dataObjectRepository = dataObjectRepository;
            this.repositoryService = repositoryService;
        }

        /// <summary>
        /// Creates or updates a data object under repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="dataObject">Data object update.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>CreateOrUpdate response for data object.</returns>
        public async Task<CreateOrUpdateReponse<DataObjectEntity>> CreateOrUpdateUnderRepositoryAsync(string dataObjectId, string repositoryId, DataObjectEntity dataObject)
        {
            CreateOrUpdateReponse<DataObjectEntity> response = new CreateOrUpdateReponse<DataObjectEntity>();
            await this.repositoryService.GetRepositoryAsync(repositoryId).ConfigureAwait(false);
            bool shouldCreate = false;

            try
            {
                DataObjectEntity dataObjectEntity = await this.GetDataObjectUnderRepositoryAsync(dataObjectId, repositoryId).ConfigureAwait(false);
            }
            catch (NotFoundException ex)
            {
                shouldCreate = true;
            }

            if (shouldCreate) 
            {
                response.IsCreated = true;
                response.Value = await this.dataObjectRepository.CreateDataObjectUnderRepositoryAsync(dataObjectId, repositoryId, dataObject).ConfigureAwait(false);
                return response;
            }

            response.IsCreated = false;
            response.Value = await this.dataObjectRepository.UpdateDataObjectUnderRepositoryAsync(dataObject, dataObjectId).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Deletes data object under a given repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <param name="dataObjectId">Data object id.</param>
        /// <returns>Deleted data object.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DataObjectEntity> DeleteUnderRepositoryAsync(string repositoryId, string dataObjectId)
        {
            try
            {
                DataObjectEntity dataObjectEntity = await this.GetDataObjectUnderRepositoryAsync(dataObjectId, repositoryId).ConfigureAwait(false);
                await this.dataObjectRepository.DeleteDataObjectUnderRepositoryAsync(dataObjectId, repositoryId).ConfigureAwait(false);
                return dataObjectEntity;
            }
            catch (NotFoundException ex)
            {
                //Logger
                return null;
            }
        }

        /// <summary>
        /// Returns data objects under a repository.
        /// </summary>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists.</exception>
        /// <returns>List of data objects.</returns>
        public async Task<IEnumerable<DataObjectEntity>> GetDataObjectsUnderRepositoryAsync(string repositoryId)
        {
            return await this.dataObjectRepository.GetAllDataObjectsUnderRepositoryAsync(repositoryId).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a data object under repository.
        /// </summary>
        /// <param name="dataObjectId">Data object id.</param>
        /// <param name="repositoryId">Repository id.</param>
        /// <exception cref="NotFoundException">Thrown when the repository does not exists or data object does not exists under the given repository.</exception>
        /// <returns>Data object entity.</returns>
        public async Task<DataObjectEntity> GetDataObjectUnderRepositoryAsync(string dataObjectId, string repositoryId)
        {
            return await this.dataObjectRepository.GetDataObjectUnderRepositoryAsync(dataObjectId, repositoryId).ConfigureAwait(false);
        }
    }
}
