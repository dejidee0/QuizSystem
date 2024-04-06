using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using QuizAppSystem.Models;
using QuizAppSystem.Seeding;
using System.Net.Http;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizAppSystem.Service.Implementation;
using QuizAppSystem.Service.Interface;
using QuizAppSystem.Service;
using QuizAppSystem.Repository;
using QuizAppSystem.Repositories.Implementation;
using QuizAppSystem.Repositories;
using QuizAppSystem.Repository.Implementation;
using QuizAppSystem.Repository.Interface;
using Microsoft.Extensions.Configuration;
using AutoMapper; // Add this using directive
using QuizAppSystem.DTOs;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Inject QuizAppDbContext
builder.Services.AddDbContext<QuizAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

// Add ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<QuizAppDbContext>()
    .AddDefaultTokenProviders();

//Configre CORS
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("https://localhost:7126", "https://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{

});



builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:ApiSettings:Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("QuizApp_BearerAuth", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API. \n Enter 'Bearer [token]' in the input below"
    }); ;
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "QuizApp_BearerAuth"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            }, new List<string>() }
    });
});

// Register IEmailSender
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register EmailService with HttpClient
builder.Services.AddHttpClient<EmailService>(client =>
{
    // Use the configuration directly
    client.BaseAddress = new Uri(builder.Configuration["EmailSettings:EmailServiceUrl"]);
});

// Configure EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();

// Replace the following lines with the actual implementations of your repository classes
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseCors("corspolicy");
app.UseHttpsRedirection();

app.UseRouting();

// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Role creation
//DataSeeder.InitializeRoles(app.Services).GetAwaiter().GetResult();
DataSeeder.InitializeRolesAndAdmin(app.Services).GetAwaiter().GetResult();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
