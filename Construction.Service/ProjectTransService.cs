using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class ProjectTransService : IProjectTransService
    {
        private readonly IProjectTransRepository _projectTransRepository;

        public ProjectTransService(IProjectTransRepository projectTransRepository)
        {
            _projectTransRepository = projectTransRepository;
        }

        public async Task<IEnumerable<ProjectTrans>> GetAllProjectTransAsync()
        {
            return await _projectTransRepository.GetAllAsync();
        }

        public async Task<ProjectTrans> GetProjectTransByIdAsync(long id)
        {
            return await _projectTransRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProjectTrans>> GetProjectTransByProjectIdAsync(long projectId)
        {
            return await _projectTransRepository.GetByProjectIdAsync(projectId);
        }

        public async Task<IEnumerable<ProjectTrans>> GetProjectTransByLevelIdAsync(long levelId)
        {
            return await _projectTransRepository.GetByLevelIdAsync(levelId);
        }

        public async Task<IEnumerable<ProjectTrans>> GetProjectTransByItemIdAsync(long itemId)
        {
            return await _projectTransRepository.GetByItemIdAsync(itemId);
        }

        public async Task<IEnumerable<ProjectTrans>> GetProjectTransByAccountTypeAsync(string accountType)
        {
            return await _projectTransRepository.GetByAccountTypeAsync(accountType);
        }

        public async Task<long> CreateProjectTransAsync(ProjectTrans projectTrans)
        {
            projectTrans.CreatedOn = DateTime.Now;
            return await _projectTransRepository.AddAsync(projectTrans);
        }

        public async Task<bool> UpdateProjectTransAsync(ProjectTrans projectTrans)
        {
            projectTrans.ModifiedOn = DateTime.Now;
            return await _projectTransRepository.UpdateAsync(projectTrans);
        }

        public async Task<bool> DeleteProjectTransAsync(long id)
        {
            return await _projectTransRepository.DeleteAsync(id);
        }
    }
}
