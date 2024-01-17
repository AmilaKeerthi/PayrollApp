using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayrollAPI.Business.Core;
using PayrollAPI.Business.Services;
using PayrollAPI.Domain.DTO;
using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using PayrollAPI.Infrastructure.Data;
using PayrollAPI.Infrastructure.Repositories;
using PayrollAPI.Utils.Services;
using PayrollAPI.Utils.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<EmployeeDTO, Employee>();
    mc.CreateMap<Employee, EmployeeDTO>();
    mc.CreateMap<SalaryPaymentDTO, SalaryPayment>();
    mc.CreateMap<SalaryPayment, SalaryPaymentDTO>();

});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200");
                      });
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISalaryPaymentRepository, SalaryPaymentRepository>();
builder.Services.AddScoped<ISalaryPaymentService, SalaryPaymentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(
  options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
      );

app.UseAuthorization();

app.MapControllers();

app.Run();
