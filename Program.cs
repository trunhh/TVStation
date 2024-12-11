using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TVStation.Data.Model;
using TVStation.Data.Constant;
using TVStation.Repositories.IRepositories;
using TVStation.Repositories.Repositories;
using TVStation.Repositories.Repositories.PlanRepositories;
using TVStation.Repositories.Repositories.PlanRepositories.ProductionPlanRepositories;
using TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories;
using TVStation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        //policy.WithOrigins("http://localhost:3000") // React app URL
         policy.AllowAnyOrigin()
                .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRole.Admin, policy => policy.RequireRole(UserRole.Admin));
    options.AddPolicy(UserRole.Director, policy => policy.RequireRole(UserRole.Director));
    options.AddPolicy(UserRole.Manager, policy => policy.RequireRole(UserRole.Manager));
    options.AddPolicy(UserRole.Reporter, policy => policy.RequireRole(UserRole.Reporter));
});

builder.Services.AddScoped<IMediaProjectRepository, MediaProjectRepository>();
builder.Services.AddScoped<IProductionRegistrationRepository, ProductionRegistrationRepository>();
builder.Services.AddScoped<IScriptProgramRepository, ScriptProgramRepository>();
builder.Services.AddScoped<IProgramFrameWeekRepository, ProgramFrameWeekRepository>();
builder.Services.AddScoped<IProgramFrameBroadcastRepository, ProgramFrameBroadcastRepository>();
builder.Services.AddScoped<IProgramFrameYearRepository, ProgramFrameYearRepository>();
builder.Services.AddScoped<ISiteMapRepository, SiteMapRepository>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.WebHost.UseWebRoot("wwwroot");
var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
