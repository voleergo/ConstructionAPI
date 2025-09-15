using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(long id);
        Task<Project> GetProjectByCodeAsync(string projectCode);
        Task<IEnumerable<Project>> GetProjectsByCustomerAsync(long customerId);
        Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status);
        Task<long> CreateProjectAsync(Project project);
        Task<bool> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(long id);
    }
}
