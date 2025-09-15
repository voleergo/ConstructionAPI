using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface ILevelRepository : IGenericRepository<Level>
    {
        Task<Level> GetByLevelCodeAsync(string levelCode);
        Task<IEnumerable<Level>> GetLevelsByStatusAsync(string status);
    }
}
