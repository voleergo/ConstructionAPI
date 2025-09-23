using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectService
    {
        public string? ConnectionStrings { get; set; }
        Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
        Task<ProjectModel> GetProjectByIdAsync(long id);
        Task<ProjectModel> GetProjectByCodeAsync(string projectCode);
        Task<IEnumerable<ProjectModel>> GetProjectsByCustomerAsync(long customerId);
        Task<IEnumerable<ProjectModel>> GetProjectsByStatusAsync(string status);
        Task<long> CreateProjectAsync(ProjectModel project);
        Task<bool> UpdateProjectAsync(ProjectModel project);
        Task<bool> DeleteProjectAsync(long id);
    }
}
