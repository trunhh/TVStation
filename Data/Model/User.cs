using Microsoft.AspNetCore.Identity;

namespace TVStation.Data.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public SiteMap? SiteMap { get; set; }
    }
}
