using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;

        public LevelService(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<IEnumerable<Level>> GetAllLevelsAsync()
        {
            return await _levelRepository.GetAllAsync();
        }

        public async Task<Level> GetLevelByIdAsync(long id)
        {
            return await _levelRepository.GetByIdAsync(id);
        }

        public async Task<Level> GetLevelByCodeAsync(string levelCode)
        {
            return await _levelRepository.GetByLevelCodeAsync(levelCode);
        }

        public async Task<IEnumerable<Level>> GetLevelsByStatusAsync(string status)
        {
            return await _levelRepository.GetLevelsByStatusAsync(status);
        }

        public async Task<long> CreateLevelAsync(Level level)
        {
            level.CreatedOn = DateTime.Now;
            return await _levelRepository.AddAsync(level);
        }

        public async Task<bool> UpdateLevelAsync(Level level)
        {
            level.UpdatedOn = DateTime.Now;
            return await _levelRepository.UpdateAsync(level);
        }

        public async Task<bool> DeleteLevelAsync(long id)
        {
            return await _levelRepository.DeleteAsync(id);
        }
    }
}
