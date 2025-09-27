using Construction.DomainModel;
using Construction.DomainModel.User;
using Construction.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Construction.DomainModel.Item;
using Construction.DomainModel.Project;
using Construction.Service;

namespace Construction.API.Controllers
{
    [Route("v1/[action]")]
    [ApiController]
    public class ItemController : BaseController
    {
        private const string connectstring = "ConnectionString:DefaultConnection";

        private readonly IConfiguration _configuration;
        private readonly IItemService _itemService;
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;
        private readonly IWebHostEnvironment _environment;


        public ItemController(
            IConfiguration configuration,
            IItemService itemService,
            ITokenService tokenService,
            ILoggerService loggerService,
            IWebHostEnvironment environment

        ) : base(configuration)
        {
            _configuration = configuration;
            _itemService = itemService;
            _tokenService = tokenService;
            _logger = loggerService;
            _environment = environment;

            _itemService.ConnectionStrings = configuration[connectstring];
            _logger.ConnectionStrings = configuration[connectstring];


        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("ProjectService")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetProjectServices(int fkProject, int idProjectService = 0)
        {
            List<ProjectServiceModel> result = new List<ProjectServiceModel>();
            IActionResult response = Unauthorized();
            try
            {
                ProjectServiceModel service = new ProjectServiceModel();
                service.FK_Project = fkProject;
                service.ID_ProjectService = idProjectService;
                result = _itemService.GetProjectServices(service);

                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No project Service found"

                    });
                }
                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while displaying the project.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("ProjectService")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdateProjectService([FromBody] ProjectServiceModel service)
        {
            HttpResponses response = new HttpResponses();
            try
            {
                if (service == null)
                {
                    return BadRequest(new
                    {
                        ResponseCode = 0,
                        ResponseMessage = "Invalid input data",
                        ResponseStatus = false
                    });
                }

                response = _itemService.UpdateProjectService(service);

                return Ok(new
                {
                    ResponseCode = response.ResponseCode,
                    ResponseMessage = response.ResponseMessage,
                    ResponseStatus = response.ResponseStatus,
                    ResponseProjectID = response.ResponseID,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Project Service Upsert");
                return BadRequest(new
                {
                    ResponseCode = 0,
                    ResponseMessage = ex.Message,
                    ResponseStatus = false
                });
            }
        }

        [HttpDelete]
        [ActionName("ProjectService")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]

        public IActionResult DeleteProjectService(int idProjectService)
        {
            try
            {
                var result = _itemService.DeleteProjectService(idProjectService);
                return Ok(new
                {
                    ResponseMessage = result.ResponseMessage,
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while deleting the project.",
                    Error = ex.Message
                });
            }
        }

        //Service Category----------------------------------------------------

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("ServiceCategory")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetServiceCategory(int Id_ServiceCategory = 0, int FK_ProjectType = 0)
        {
            List<Item> result = new List<Item>();
            try
            {
                result = _itemService.GetServiceCategory(new Item
                {
                    ID_ServiceCategory = Id_ServiceCategory,
                    FK_ProjectType = FK_ProjectType
                });

                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No service category found"
                    });
                }

                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while fetching service categories.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [ActionName("ServiceCategory")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdateServiceCategory([FromBody] Item service)
        {
            HttpResponses response = new HttpResponses();
            try
            {
                if (service == null)
                {
                    return BadRequest(new
                    {
                        ResponseCode = 0,
                        ResponseMessage = "Invalid input data",
                        ResponseStatus = false
                    });
                }

                response = _itemService.UpdateServiceCategory(service);

                return Ok(new
                {
                    ResponseCode = response.ResponseCode,
                    ResponseMessage = response.ResponseMessage,
                    ResponseStatus = response.ResponseStatus,
                    ResponseProjectID = response.ResponseID,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Project Service Upsert");
                return BadRequest(new
                {
                    ResponseCode = 0,
                    ResponseMessage = ex.Message,
                    ResponseStatus = false
                });
            }
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("Supplier")]
        [ApiExplorerSettings(IgnoreApi = false)]
         public IActionResult GetSuppliers(int idSupplier = 0, int fkServiceCategory = 0)
            {
                List<SupplierModel> result = new List<SupplierModel>();
                IActionResult response = Unauthorized();
                try
                {
                    SupplierModel supplier = new SupplierModel();
                    supplier.ID_Supplier = idSupplier;
                    supplier.FK_ServiceCategory = fkServiceCategory;
                    result = _itemService.GetSuppliers(supplier);

                    if (result == null || result.Count == 0)
                    {
                        return NotFound(new
                        {
                            Message = "No suppliers found"
                        });
                    }
                    return Ok(new
                    {
                        Result = result
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        ResponseCode = "500",
                        ResponseMessage = "An error occurred while retrieving suppliers.",
                        Error = ex.Message
                    });
                }
            }


        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("Supplier")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplierModel model)
        {
            try
            {
                var result = _itemService.AddSupplier(model);
                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add Supplier");

                // Even on exception, send proper response format like SP does
                return Ok(new
                {
                    Result = new
                    {
                        ResponseCode = -1,
                        ResponseMessage = ex.Message,
                        ResponseStatus = 0,
                        ResponseID = model.ID_Supplier
                    }
                });
            }
        }
    }
}

