using CarCompany.Application.Interfaces;
using CarCompany.Application.RepositoriesContracts;
using CarCompany.Application.Services;
using CarCompany.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddSingleton<ISalesRepositoryContract, SalesRepository>();
builder.Services.AddSingleton<IDistributionCenterRepositoryContract, DistributionCenterRepository>();

// Register services
builder.Services.AddSingleton<ISalesService, SalesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();
app.MapControllers();

app.Run();