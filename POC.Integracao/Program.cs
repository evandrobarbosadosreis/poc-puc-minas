using Microsoft.EntityFrameworkCore;
using POC.Integracao.Infrastructure;
using POC.Integracao.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("db"), ServiceLifetime.Singleton);
builder.Services.AddScoped<IIntegrationRepository, IntegrationRepository>();
builder.Services.AddScoped<ISQSService, SQSService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();