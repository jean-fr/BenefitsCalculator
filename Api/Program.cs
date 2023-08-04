using Api.Business;
using Api.Business.Interfaces;
using Api.Daos;
using Api.Daos.Interfaces;
using Api.Dtos;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

builder.Services.AddTransient<IEmployeeDao, EmployeeDao>();
builder.Services.AddTransient<IDependentDao, DependentDao>();
builder.Services.AddTransient<IEmployeeBusiness, EmployeeBusiness>();
builder.Services.AddTransient<IDependentBusiness, DependentBusiness>();


var mapperConfiguration = new MapperConfiguration(cfg =>
{
   cfg.CreateMap<ModelBase, PLModelBase>()
    .IncludeAllDerived();

    cfg.CreateMap<Employee, PLEmployee>().ForMember(d => d.Dependents, act => act.Ignore());
    cfg.CreateMap<Dependent, PLDependent>().ForMember(d => d.Employee, act => act.Ignore());

});
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "paylocitybenefitscalculator Api v1"));


app.UseCors(allowLocalhost);

app.UseAuthorization();

app.MapControllers();

app.Run();
