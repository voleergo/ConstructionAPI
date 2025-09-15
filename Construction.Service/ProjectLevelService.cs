using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class ProjectLevelService : IProjectLevelService
    {
        private readonly IProjectLevelRepository _projectLevelRepository;

        public ProjectLevelService(IProjectLevelRepository projectLevelRepository)
        {
            _projectLevelRepository = projectLevelRepository;
        }

        public async Task<IEnumerable<ProjectLevel>> GetAllProjectLevelsAsync()
        {
            return await _projectLevelRepository.GetAllAsync();
        }

        public async Task<ProjectLevel> GetProjectLevelByIdAsync(long id)
        {
            return await _projectLevelRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProjectLevel>> GetProjectLevelsByProjectIdAsync(long projectId)
        {
            return await _projectLevelRepository.GetByProjectIdAsync(projectId);
        }

        public async Task<IEnumerable<ProjectLevel>> GetProjectLevelsByLevelIdAsync(long levelId)
        {
            return await _projectLevelRepository.GetByLevelIdAsync(levelId);
        }

        public async Task<long> CreateProjectLevelAsync(ProjectLevel projectLevel)
        {
            return await _projectLevelRepository.AddAsync(projectLevel);
        }

        public async Task<bool> UpdateProjectLevelAsync(ProjectLevel projectLevel)
        {
            return await _projectLevelRepository.UpdateAsync(projectLevel);
        }

        public async Task<bool> DeleteProjectLevelAsync(long id)
        {
            return await _projectLevelRepository.DeleteAsync(id);
        }
    }
}
