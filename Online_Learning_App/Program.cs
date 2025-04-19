//using Application.Interfaces;
using Application.Services;
//using Domain.Interfaces;
//using Infrastructure.Data;
//using Infrastructure.Repositories;
using AutoMapper;


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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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
});
app.MapHub<NotificationHub>("/notificationhub");

app.MapControllers();
app.Run();