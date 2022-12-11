using DataStorageAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace DataStorageAPI.ApiLayer.Models
{
    /// <summary>
    /// API model for Data objects.
    /// </summary>
    public class DataObject
    {
        [MaxLength(CommonConstants.MaxIdLength)]
        [MinLength(CommonConstants.MinIdLength)]
        public string? Id { get; set; }

        [Required]
        public string? Name { get; set;  }

        public long Size { get; set; }
    }
}
