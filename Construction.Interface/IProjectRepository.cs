using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectRepository : IGenericRepository<ProjectModel>
    {
        Task<ProjectModel> GetByProjectCodeAsync(string projectCode);
        Task<IEnumerable<ProjectModel>> GetProjectsByCustomerAsync(long customerId);
        Task<IEnumerable<ProjectModel>> GetProjectsByStatusAsync(string status);
    }
}
