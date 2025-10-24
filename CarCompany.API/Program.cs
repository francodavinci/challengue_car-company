// CarCompany.API/Program.cs
using CarCompany.Application.UseCases;
using CarCompany.Domain.Interfaces;
using CarCompany.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories (Domain interfaces)
builder.Services.AddSingleton<ISalesRepository, SalesRepository>();
builder.Services.AddSingleton<IDistributionCenterRepository, DistributionCenterRepository>();

// Register use cases
builder.Services.AddScoped<CreateSaleUseCase>();
builder.Services.AddScoped<GetTotalSalesUseCase>();
builder.Services.AddScoped<GetSalesByDistributionCenterUseCase>();
builder.Services.AddScoped<GetUnitsSalesPercentageByDistributionCenter>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();