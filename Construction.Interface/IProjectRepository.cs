using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project> GetByProjectCodeAsync(string projectCode);
        Task<IEnumerable<Project>> GetProjectsByCustomerAsync(long customerId);
        Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status);
    }
}
