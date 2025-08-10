using Api.Interfaces;
using Api.Models;
using Api.ProfileMap;
using Api.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDependentService, DependentService>();
builder.Services.Configure<PaycheckSettings>(builder.Configuration.GetSection("PaycheckSettings"));
builder.Services.AddScoped<IPayCheckService, PaycheckService>();
builder.Services.AddScoped<IBenefitCostCalculator, BaseBenefitCostCalculator>();
builder.Services.AddScoped<IBenefitCostCalculator, AgeBasedBenefitCostCalculator>();
builder.Services.AddScoped<IBenefitCostCalculator, DependentBenefitCostCalculator>();
builder.Services.AddScoped<IBenefitCostCalculator, AdditionalBenefitCostCalculator>();
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

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
        policy =>  policy.WithOrigins("http://localhost:44376")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
