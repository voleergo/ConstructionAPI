using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectTransRepository : IGenericRepository<ProjectTrans>
    {
        Task<IEnumerable<ProjectTrans>> GetByProjectIdAsync(long projectId);
        Task<IEnumerable<ProjectTrans>> GetByLevelIdAsync(long levelId);
        Task<IEnumerable<ProjectTrans>> GetByItemIdAsync(long itemId);
        Task<IEnumerable<ProjectTrans>> GetByAccountTypeAsync(string accountType);
    }
}
