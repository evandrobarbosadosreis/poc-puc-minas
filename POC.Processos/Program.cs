using Microsoft.EntityFrameworkCore;
using POC.Processos.Consumer;
using POC.Processos.Integration;
using POC.Processos.Repository;
using POC.Processos.SQSService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("db"), ServiceLifetime.Singleton);
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<ITribunalApi, TribunalSerivce>();
builder.Services.AddScoped<ISQSService, SQSService>();

builder.Services.AddHostedService<SQSConsumer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();