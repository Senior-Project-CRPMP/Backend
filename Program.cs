using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Data;
using Backend.Interfaces.Account;
using Backend.Models.Account;
using Backend.Repositories.Account;
using Microsoft.OpenApi.Models;
using Backend.Interfaces.Document;
using Backend.Interfaces.Form;
using Backend.Repository.Document;
using Backend.Repository.Form;
using Backend.Repository.FormQuestion;
using Backend.Repository;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;
using Backend.Repositories;
using Backend.Interfaces.FileUpload;
using Backend.Interfaces.Project;
using Backend.Repository.Project;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
//builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IFormQuestionRepository, FormQuestionRepository>();
builder.Services.AddScoped<IFormOptionRepository, FormOptionRepository>();
builder.Services.AddScoped<IFormFileStorageRepository, FormFileStorageRepository>();
builder.Services.AddScoped<IFormResponseRepository, FormResponseRepository>();
builder.Services.AddScoped<IFormAnswerRepository, FormAnswerRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();


builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();
builder.Services.AddScoped<IProfilePicUploadRepository, ProfilePicUploadRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});

builder.Services.AddSignalR();

builder.Services.AddCors(option => option.AddPolicy("AllowSpecificOrigin", builder =>
{
    builder.WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
}));

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Initialize roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await EnsureRolesAsync(roleManager);
}

async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "Admin", "StandardUser" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
app.MapHub<ChatHub>("/Chat");
app.MapHub<NotificationHub>("/Notification");

app.Run();
