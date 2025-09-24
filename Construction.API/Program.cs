using System.Text;
using Construction.DataAccess;
using Construction.DomainModel.User;
using Construction.Interface;
using Construction.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Construction API",
        Version = "v1",
        Description = "A comprehensive API for Construction Management System",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Construction Team"
        }
    });
    
    // Enable XML comments if you want to add them later
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

// Register connection helper
builder.Services.AddSingleton<DatabaseConnectionHelper>();

// Register repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ILevelRepository, LevelRepository>();
builder.Services.AddScoped<IProjectLevelRepository, ProjectLevelRepository>();
builder.Services.AddScoped<IProjectTransRepository, ProjectTransRepository>();
builder.Services.Configure<OTPConfig>(
    builder.Configuration.GetSection("OTPConfig"));


// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ILevelService, LevelService>();
builder.Services.AddScoped<IProjectLevelService, ProjectLevelService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IProjectTransService, ProjectTransService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-auth-server.com";
        options.Audience = "your-api-resource";
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = JwtSecurityParams.jwtValidAudience,
            ValidIssuer = JwtSecurityParams.jwtValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurityParams.jwtSecret))
        };
    });
// Add CORS - Cross Origin Resource Sharing for all endpoints
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
    // Match controller attributes using "AllowOrigin"
    options.AddPolicy("AllowOrigin", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Construction API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at app's root
        c.DocumentTitle = "Construction API Documentation";
        c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseRouting();
// Enable CORS - Must be called before UseAuthorization
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
