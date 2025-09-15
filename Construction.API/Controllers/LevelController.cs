using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Level>>> GetAllLevels()
        {
            try
            {
                var levels = await _levelService.GetAllLevelsAsync();
                return Ok(levels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Level>> GetLevel(long id)
        {
            try
            {
                var level = await _levelService.GetLevelByIdAsync(id);
                if (level == null)
                {
                    return NotFound($"Level with ID {id} not found.");
                }
                return Ok(level);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-code/{levelCode}")]
        public async Task<ActionResult<Level>> GetLevelByCode(string levelCode)
        {
            try
            {
                var level = await _levelService.GetLevelByCodeAsync(levelCode);
                if (level == null)
                {
                    return NotFound($"Level with code {levelCode} not found.");
                }
                return Ok(level);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-status/{status}")]
        public async Task<ActionResult<IEnumerable<Level>>> GetLevelsByStatus(string status)
        {
            try
            {
                var levels = await _levelService.GetLevelsByStatusAsync(status);
                return Ok(levels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateLevel([FromBody] Level level)
        {
            try
            {
                if (level == null)
                {
                    return BadRequest("Level data is required.");
                }

                var levelId = await _levelService.CreateLevelAsync(level);
                return CreatedAtAction(nameof(GetLevel), new { id = levelId }, levelId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLevel(long id, [FromBody] Level level)
        {
            try
            {
                if (level == null)
                {
                    return BadRequest("Level data is required.");
                }

                if (id != level.ID_Level)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _levelService.UpdateLevelAsync(level);
                if (!result)
                {
                    return NotFound($"Level with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLevel(long id)
        {
            try
            {
                var result = await _levelService.DeleteLevelAsync(id);
                if (!result)
                {
                    return NotFound($"Level with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
