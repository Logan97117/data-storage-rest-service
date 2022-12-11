using DataStorageAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace DataStorageAPI.ApiLayer.Models
{
    /// <summary>
    /// API model for repository
    /// </summary>
    public class Repository
    {
        [MaxLength(CommonConstants.MaxIdLength)]
        [MinLength(CommonConstants.MinIdLength)]
        public string Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public Dictionary<string, string>? Metadata { get;set; }
    }
}
