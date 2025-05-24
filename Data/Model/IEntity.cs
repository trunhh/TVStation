using System.Text.Json.Serialization;

namespace TVStation.Data.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedDate { get; set; }
        [JsonIgnore]
        bool IsDeleted { get; set; }
    }
}
