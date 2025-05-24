using System.Text.Json.Serialization;

namespace TVStation.Data.Model
{
    public class SiteMap : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public List<User> Members {  get; set; } = new List<User>();
    }
}
