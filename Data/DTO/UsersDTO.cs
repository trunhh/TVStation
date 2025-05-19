using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class UsersDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name => UserName;
    }
}
