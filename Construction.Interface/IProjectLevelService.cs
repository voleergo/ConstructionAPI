using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectLevelService
    {
        Task<IEnumerable<ProjectLevel>> GetAllProjectLevelsAsync();
        Task<ProjectLevel> GetProjectLevelByIdAsync(long id);
        Task<IEnumerable<ProjectLevel>> GetProjectLevelsByProjectIdAsync(long projectId);
        Task<IEnumerable<ProjectLevel>> GetProjectLevelsByLevelIdAsync(long levelId);
        Task<long> CreateProjectLevelAsync(ProjectLevel projectLevel);
        Task<bool> UpdateProjectLevelAsync(ProjectLevel projectLevel);
        Task<bool> DeleteProjectLevelAsync(long id);
    }
}
