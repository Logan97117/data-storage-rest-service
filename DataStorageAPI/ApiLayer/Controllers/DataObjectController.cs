using DataStorageAPI.ApiLayer.Models;
using DataStorageAPI.ApiLayer.Models.ModelMappers;
using DataStorageAPI.Common;
using DataStorageAPI.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataStorageAPI.ApiLayer.Controllers
{
    /// <summary>
    /// Controller for data object resource.
    /// </summary>
    [Route("api/repository/{repositoryId}/dataobjects")]
    [ApiController]
    public class DataObjectController : ControllerBase
    {
        private readonly IDataObjectService dataObjectService;
        public DataObjectController(IDataObjectService dataObjectService) 
        {
            this.dataObjectService = dataObjectService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataObject>>> GetDataObjectsUnderRepository(
            [FromRoute] string repositoryId)
        {
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(repositoryId, nameof(repositoryId));
            var dataObjectEntities = await this.dataObjectService.GetDataObjectsUnderRepositoryAsync(repositoryId).ConfigureAwait(false);
            var dataObjectDtos = dataObjectEntities.Select(dataObjectEntity => DataObjectMapper.Map(dataObjectEntity));
            return this.Ok(dataObjectDtos);
        }

        [HttpGet("{dataObjectId}", Name = Routenames.DataObjectById)]
        public async Task<ActionResult<DataObject>> GetDataObjectUnderRepositoryById(
            [FromRoute] string repositoryId,
            [FromRoute] string dataObjectId)
        {
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(repositoryId, nameof(repositoryId));
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(dataObjectId, nameof(dataObjectId));
            var dataObjectEntity = await this.dataObjectService.GetDataObjectUnderRepositoryAsync(dataObjectId, repositoryId).ConfigureAwait(false);
            var dataObjectDto = DataObjectMapper.Map(dataObjectEntity);
            return this.Ok(dataObjectDto);
        }

        [HttpPut("{dataObjectId}")]
        public async Task<ActionResult<DataObject>> CreateDataObject(
            [FromRoute] string repositoryId,
            [FromRoute] string dataObjectId,
            [FromBody] DataObject dataObject)
        {
            ArguementUtility.CheckForNull(dataObject, nameof(dataObject));
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(repositoryId, nameof(repositoryId));
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(dataObjectId, nameof(dataObjectId));
            var dataObjectEntity = DataObjectMapper.Map(dataObject);
            var response = await this.dataObjectService.CreateOrUpdateUnderRepositoryAsync(dataObjectId, repositoryId, dataObjectEntity).ConfigureAwait(false);
            var mappedResponse = DataObjectMapper.Map(response.Value);

            if (response.IsCreated)
            {
                return this.CreatedAtRoute(Routenames.DataObjectById, 
                    new { repositoryId, dataObjectId = mappedResponse.Id}, mappedResponse);
            }

            return this.Ok(mappedResponse);
        }

        [HttpDelete("{dataObjectId}")]
        public async Task<IActionResult> DeleteDataObject(
            [FromRoute] string repositoryId,
            [FromRoute] string dataObjectId)
        {
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(repositoryId, nameof(repositoryId));
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(dataObjectId, nameof(dataObjectId));
            var deletedDataObjectEntity = await this.dataObjectService.DeleteUnderRepositoryAsync(repositoryId, dataObjectId).ConfigureAwait(false);

            if (deletedDataObjectEntity == null)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
