namespace DataStorageAPI.ServiceLayer.Models
{
    public class RepositoryEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Metadata { get;set; } = new Dictionary<string, string>();
    }
}
