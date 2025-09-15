using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectTransService
    {
        Task<IEnumerable<ProjectTrans>> GetAllProjectTransAsync();
        Task<ProjectTrans> GetProjectTransByIdAsync(long id);
        Task<IEnumerable<ProjectTrans>> GetProjectTransByProjectIdAsync(long projectId);
        Task<IEnumerable<ProjectTrans>> GetProjectTransByLevelIdAsync(long levelId);
        Task<IEnumerable<ProjectTrans>> GetProjectTransByItemIdAsync(long itemId);
        Task<IEnumerable<ProjectTrans>> GetProjectTransByAccountTypeAsync(string accountType);
        Task<long> CreateProjectTransAsync(ProjectTrans projectTrans);
        Task<bool> UpdateProjectTransAsync(ProjectTrans projectTrans);
        Task<bool> DeleteProjectTransAsync(long id);
    }
}
