namespace TVStation.Data.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
