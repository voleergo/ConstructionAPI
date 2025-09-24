using Construction.DomainModel;
using Construction.DomainModel.User;
using Construction.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Construction.DomainModel.Item;

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
        
    }
}
