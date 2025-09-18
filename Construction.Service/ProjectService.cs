using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<ProjectModel> GetProjectByIdAsync(long id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<ProjectModel> GetProjectByCodeAsync(string projectCode)
        {
            return await _projectRepository.GetByProjectCodeAsync(projectCode);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectsByCustomerAsync(long customerId)
        {
            return await _projectRepository.GetProjectsByCustomerAsync(customerId);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectsByStatusAsync(string status)
        {
            return await _projectRepository.GetProjectsByStatusAsync(status);
        }

        public async Task<long> CreateProjectAsync(ProjectModel project)
        {
            project.CreatedOn = DateTime.Now;
            return await _projectRepository.AddAsync(project);
        }

        public async Task<bool> UpdateProjectAsync(ProjectModel project)
        {
            project.UpdatedOn = DateTime.Now;
            return await _projectRepository.UpdateAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(long id)
        {
            return await _projectRepository.DeleteAsync(id);
        }
    }
}
