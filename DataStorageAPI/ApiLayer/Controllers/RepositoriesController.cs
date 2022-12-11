using DataStorageAPI.ApiLayer.Models;
using DataStorageAPI.ApiLayer.Models.ModelMappers;
using DataStorageAPI.Common;
using DataStorageAPI.ServiceLayer.Interfaces;
using DataStorageAPI.ServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataStorageAPI.ApiLayer.Controllers
{
    /// <summary>
    /// Controller for repository resource
    /// </summary>
    [Route("api/repository")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        private readonly IRepositoryService repositoryService;
        public RepositoriesController(IRepositoryService repositoryService) 
        {
            this.repositoryService = repositoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repository>>> GetAllRepositories()
        {
            IEnumerable<RepositoryEntity> repositoryList = await this.repositoryService.GetAllRepositoriesAsync().ConfigureAwait(false);
            IEnumerable<Repository> repositories = repositoryList.Select((repositoryEntity) => RepositoryMapper.Map(repositoryEntity));
            return this.Ok(repositories);
        }

        [HttpGet("{repositoryId}", Name = Routenames.RepoById)]
        public async Task<ActionResult<Repository>> GetRepositoryById([FromRoute] string repositoryId)
        {
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(repositoryId, nameof(repositoryId));
            RepositoryEntity repositoryEntity = await this.repositoryService.GetRepositoryAsync(repositoryId).ConfigureAwait(false);
            Repository response = RepositoryMapper.Map(repositoryEntity);
            return this.Ok(response);
        }

        [HttpPut("{repositoryId}")]
        public async Task<ActionResult<Repository>> CreateRepository(
            [FromBody] Repository repositoryUpdate, 
            [FromRoute] string repositoryId) 
        {
            ArguementUtility.CheckForNull(repositoryUpdate, nameof(repositoryUpdate));
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(repositoryId, nameof(repositoryId));
            RepositoryEntity repository = RepositoryMapper.Map(repositoryUpdate);
            var response = await this.repositoryService.CreateOrUpdateRepositoryAsync(repositoryId, repository).ConfigureAwait(false);
            var mappedResponse = RepositoryMapper.Map(response.Value);

            if (response.IsCreated)
            {
                return this.CreatedAtRoute(Routenames.RepoById, 
                    new { repositoryId = mappedResponse.Id }, mappedResponse);
            }

            return this.Ok(mappedResponse);
        }

        [HttpDelete("{repositoryId}")]
        public async Task<IActionResult> DeleteRepository([FromRoute] string repositoryId) 
        {
            ArguementUtility.CheckForStringNullOrEmptyOrWhiteSpace(repositoryId, nameof(repositoryId));
            RepositoryEntity deletedEntity = await this.repositoryService.DeleteRepositoryAsync(repositoryId).ConfigureAwait(false);

            if (deletedEntity == null)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
