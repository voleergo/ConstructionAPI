using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            try
            {
                var items = await _itemService.GetAllItemsAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            try
            {
                var item = await _itemService.GetItemByIdAsync(id);
                if (item == null)
                {
                    return NotFound($"Item with ID {id} not found.");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-code/{itemCode}")]
        public async Task<ActionResult<Item>> GetItemByCode(string itemCode)
        {
            try
            {
                var item = await _itemService.GetItemByCodeAsync(itemCode);
                if (item == null)
                {
                    return NotFound($"Item with code {itemCode} not found.");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-type/{itemType}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByType(string itemType)
        {
            try
            {
                var items = await _itemService.GetItemsByTypeAsync(itemType);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateItem([FromBody] Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Item data is required.");
                }

                var itemId = await _itemService.CreateItemAsync(item);
                return CreatedAtAction(nameof(GetItem), new { id = itemId }, itemId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(long id, [FromBody] Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Item data is required.");
                }

                if (id != item.ID_Item)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _itemService.UpdateItemAsync(item);
                if (!result)
                {
                    return NotFound($"Item with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            try
            {
                var result = await _itemService.DeleteItemAsync(id);
                if (!result)
                {
                    return NotFound($"Item with ID {id} not found.");
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
