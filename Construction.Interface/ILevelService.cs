using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface ILevelService
    {
        Task<IEnumerable<Level>> GetAllLevelsAsync();
        Task<Level> GetLevelByIdAsync(long id);
        Task<Level> GetLevelByCodeAsync(string levelCode);
        Task<IEnumerable<Level>> GetLevelsByStatusAsync(string status);
        Task<long> CreateLevelAsync(Level level);
        Task<bool> UpdateLevelAsync(Level level);
        Task<bool> DeleteLevelAsync(long id);
    }
}
