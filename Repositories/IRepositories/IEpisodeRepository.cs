using Microsoft.EntityFrameworkCore;
using TVStation.Data.DTO;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;

namespace TVStation.Repositories.IRepositories
{
    public interface IEpisodeRepository : IGenericRepository<Episode>
    {
        List<Episode> GetAll(EpisodeQuery query);
        List<Episode> CheckSchedulingConflict(Episode episode);
        int GetNextIndex(Guid progId);
    }
}
