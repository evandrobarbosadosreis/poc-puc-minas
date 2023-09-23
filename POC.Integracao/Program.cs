using Microsoft.EntityFrameworkCore;
using POC.Integracao.Consumer;
using POC.Integracao.Repository;
using POC.Integracao.SQSService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("db"), ServiceLifetime.Singleton);
builder.Services.AddScoped<IIntegrationRepository, IntegrationRepository>();
builder.Services.AddScoped<ISQSService, SQSService>();

builder.Services.AddHostedService<SuccessSQSConsumer>();
builder.Services.AddHostedService<FailureSQSConsumer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();