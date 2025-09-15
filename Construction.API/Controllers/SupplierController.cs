using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliers()
        {
            try
            {
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(long id)
        {
            try
            {
                var supplier = await _supplierService.GetSupplierByIdAsync(id);
                if (supplier == null)
                {
                    return NotFound($"Supplier with ID {id} not found.");
                }
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-code/{supplierCode}")]
        public async Task<ActionResult<Supplier>> GetSupplierByCode(string supplierCode)
        {
            try
            {
                var supplier = await _supplierService.GetSupplierByCodeAsync(supplierCode);
                if (supplier == null)
                {
                    return NotFound($"Supplier with code {supplierCode} not found.");
                }
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search/{supplierName}")]
        public async Task<ActionResult<IEnumerable<Supplier>>> SearchSuppliers(string supplierName)
        {
            try
            {
                var suppliers = await _supplierService.SearchSuppliersByNameAsync(supplierName);
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateSupplier([FromBody] Supplier supplier)
        {
            try
            {
                if (supplier == null)
                {
                    return BadRequest("Supplier data is required.");
                }

                var supplierId = await _supplierService.CreateSupplierAsync(supplier);
                return CreatedAtAction(nameof(GetSupplier), new { id = supplierId }, supplierId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSupplier(long id, [FromBody] Supplier supplier)
        {
            try
            {
                if (supplier == null)
                {
                    return BadRequest("Supplier data is required.");
                }

                if (id != supplier.ID_Supplier)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _supplierService.UpdateSupplierAsync(supplier);
                if (!result)
                {
                    return NotFound($"Supplier with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplier(long id)
        {
            try
            {
                var result = await _supplierService.DeleteSupplierAsync(id);
                if (!result)
                {
                    return NotFound($"Supplier with ID {id} not found.");
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
