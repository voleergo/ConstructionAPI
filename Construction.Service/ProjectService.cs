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

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project> GetProjectByIdAsync(long id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<Project> GetProjectByCodeAsync(string projectCode)
        {
            return await _projectRepository.GetByProjectCodeAsync(projectCode);
        }

        public async Task<IEnumerable<Project>> GetProjectsByCustomerAsync(long customerId)
        {
            return await _projectRepository.GetProjectsByCustomerAsync(customerId);
        }

        public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status)
        {
            return await _projectRepository.GetProjectsByStatusAsync(status);
        }

        public async Task<long> CreateProjectAsync(Project project)
        {
            project.CreatedOn = DateTime.Now;
            return await _projectRepository.AddAsync(project);
        }

        public async Task<bool> UpdateProjectAsync(Project project)
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
