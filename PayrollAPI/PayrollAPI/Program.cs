using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PayrollAPI.Business.Core;
using PayrollAPI.Business.Services;
using PayrollAPI.Domain.DTO;
using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using PayrollAPI.Infrastructure.Data;
using PayrollAPI.Infrastructure.Repositories;
using PayrollAPI.Infrastructure.SeedData;
using PayrollAPI.Utils.Services;
using PayrollAPI.Utils.Settings;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBConnection")));
builder.Services.AddScoped<DbInitializer>();
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
        ValidIssuer = builder.Configuration.GetSection("AuthSettings").GetValue("Issuer", ""),
        ValidAudience = builder.Configuration.GetSection("AuthSettings").GetValue("Audience", ""),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AuthSettings").GetValue("Secrect", "")))
    };
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<EmployeeDTO, Employee>();
    mc.CreateMap<Employee, EmployeeDTO>();
    mc.CreateMap<SalaryPaymentDTO, SalaryPayment>();
    mc.CreateMap<SalaryPayment, SalaryPaymentDTO>();
    mc.CreateMap<SalaryPayment, SalaryPaymentUpdateDTO>();
    mc.CreateMap<SalaryPaymentUpdateDTO, SalaryPayment>();


});

builder.Services.AddCors();

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISalaryPaymentRepository, SalaryPaymentRepository>();
builder.Services.AddScoped<ISalaryPaymentService, SalaryPaymentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));


builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseItToSeedSqlServer();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(
  options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
      );
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
