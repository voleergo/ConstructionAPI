using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectLevelRepository : IGenericRepository<ProjectLevel>
    {
        Task<IEnumerable<ProjectLevel>> GetByProjectIdAsync(long projectId);
        Task<IEnumerable<ProjectLevel>> GetByLevelIdAsync(long levelId);
    }
}
