using DataStorageAPI.ServiceLayer.Models;

namespace DataStorageAPI.ApiLayer.Models.ModelMappers
{
    /// <summary>
    /// Mapper for Repository DTO and repository entity.
    /// </summary>
    public static partial class RepositoryMapper
    {
        /// <summary>
        /// Maps from repository entity to repository DTO.
        /// </summary>
        /// <param name="repositoryEntity">Repository entity.</param>
        /// <returns>Repository DTO.</returns>
        public static Repository Map(RepositoryEntity repositoryEntity)
        {
            return new Repository()
            {
                Id = repositoryEntity.Id,
                Name = repositoryEntity.Name,
                Metadata = repositoryEntity.Metadata,
            };
        }

        /// <summary>
        /// Maps from repository DTO to repository entity.
        /// </summary>
        /// <param name="repository">Repository DTO.</param>
        /// <returns>Repository entity.</returns>
        public static RepositoryEntity Map(Repository repository) 
        {
            return new RepositoryEntity()
            {
                Id = repository.Id,
                Name = repository.Name,
                Metadata = repository.Metadata,
            };
        }
    }
}
