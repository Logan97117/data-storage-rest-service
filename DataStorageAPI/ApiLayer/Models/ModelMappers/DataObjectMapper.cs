using DataStorageAPI.ServiceLayer.Models;

namespace DataStorageAPI.ApiLayer.Models.ModelMappers
{
    /// <summary>
    /// Mapper for data object DTO and data object entity.
    /// </summary>
    public static class DataObjectMapper
    {
        /// <summary>
        /// Returns data object entity from a given data object DTO.
        /// </summary>
        /// <param name="dataObject">Data object DTO.</param>
        /// <returns>Data object entity.</returns>
        public static DataObjectEntity Map(DataObject dataObject)
        {
            return new DataObjectEntity()
            {
                Id = dataObject.Id,
                Name = dataObject.Name,
                Size = dataObject.Size,
            };
        }

        /// <summary>
        /// Returns data object DTO from a given data object entity.
        /// </summary>
        /// <param name="dataObjectEntity">Data object entity.</param>
        /// <returns>Data object DTO</returns>
        public static DataObject Map(DataObjectEntity dataObjectEntity)
        {
            return new DataObject()
            {
                Id = dataObjectEntity.Id,
                Name = dataObjectEntity.Name,
                Size = dataObjectEntity.Size,
            };
        }
    }
}
