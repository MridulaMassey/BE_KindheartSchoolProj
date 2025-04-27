//using Application.Interfaces;
using Application.Services;
//using Domain.Interfaces;
//using Infrastructure.Data;
//using Infrastructure.Repositories;
using AutoMapper;

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Online_Learning_APP.Application.Services;
using AuthenticationApp.Application.Services;
using Online_Learning_App.Infrastructure.Repository;
using Online_Learning_App.Application.Services;
using Online_Learning_APP.Application.DTO;
using AutoMapper; // Add thi
using System.Reflection;
using Online_Learning_App.Infrastructure.Service; // Add this line
using Online_Learning_App.Domain.Interfaces;

//using System.Text.Json.Serialization;
//using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Online_Learning_APP.Application.Handler;
using Online_Learning_APP.Application.Services.Online_Learning_APP.Services;
//using Online_Learning_APP.Application.Worker;
using Online_Learning_App.Infrastructure.BackgroundWorker;
//using Online_Learning_App.Infrastructure.Background;


var builder = WebApplication.CreateBuilder(args);

// Configure database
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Online_Learning_App.Infrastructure")));


// Identity setup
builder.Services.AddIdentity<ApplicationUser, Role>(
    options => {
        options.ClaimsIdentity.UserIdClaimType = null;
        options.ClaimsIdentity.RoleClaimType = null;
    }
    )
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Cookie Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/auth/login";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var settings = new JsonSerializerSettings
{
    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    Formatting = Formatting.Indented
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddSingleton(new JsonSerializerSettings
{
    //ReferenceHandler = ReferenceHandler.Preserve,
    //WriteIndented = true
     PreserveReferencesHandling = PreserveReferencesHandling.Objects,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    Formatting = Formatting.Indented
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


builder.Services.AddControllers();
// Register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClassGroupService, ClassGroupService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IClassGroupService, ClassGroupService>();
builder.Services.AddScoped<IClassGroupSubjectService, ClassGroupSubjectService>();
builder.Services.AddScoped<IClassGroupSubjectRepository, ClassGroupSubjectRepository>();
builder.Services.AddScoped<IClassGroupSubjectActivityRepository, ClassGroupSubjectActivityRepository>();
builder.Services.AddScoped<IClassGroupSubjectStudentActivityRepository, ClassGroupSubjectStudentActivityRepository>();
builder.Services.AddScoped<IClassGroupSubjectStudentActivityService, ClassGroupSubjectStudentActivityService>();
builder.Services.AddScoped<IClassGroupSubjectActivityService, ClassGroupSubjectActivityService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Services.AddScoped<INotificationService, NotificationService>();


builder.Services.AddScoped<ICertificateRepository, CertificateRepository>(); //added merl
builder.Services.AddScoped<ICertificateService, CertificateService>(); //added merl
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddControllers();
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Update your JWT configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero,
        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" // Standard role claim type
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Allow the token to be passed in the Authorization header or as a query parameter
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ??
                        context.Request.Query["access_token"];

            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"JWT Authentication Failed: {context.Exception.Message}");
            return Task.CompletedTask;
        }
    };
});

// Configure authorization policies for roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireTeacherRole", policy => policy.RequireRole("Teacher"));
    options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("Student"));
});

// Update your Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Learning API", Version = "v1" });

    // Configure Swagger to use JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // Optional: Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configure Swagger UI to display the authorize button
//builder.Services.AddSwaggerGenNewtonsoftSupport(); // Add this if using Newtonsoft.Json
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateActivityCommand).Assembly));


builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddHostedService<FinalGradeBatchWorker>();
builder.Services.AddHttpClient();


// Add services
builder.Services.AddSingleton<IGoogleDriveService, GoogleDriveService>();
builder.Services.AddTransient<FileUploadService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); // Add this line before UseAuthorization()
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ActivityHub>("/activityHub");
});
app.MapHub<ActivityHub>("/activityHub");

app.MapControllers();
app.Run();