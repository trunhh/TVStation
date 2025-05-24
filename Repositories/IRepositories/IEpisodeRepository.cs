using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;

namespace TVStation.Repositories.IRepositories
{
    public interface IEpisodeRepository : IGenericRepository<Episode>
    {
        List<Episode> CheckSchedulingConflict(Episode episode);
    }
}
