using TVStation.Data.Model;

namespace TVStation.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
