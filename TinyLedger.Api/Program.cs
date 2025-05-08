using MediatR;
using TinyLedger.Application.UseCases;
using TinyLedger.Domain;
using TinyLedger.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILedgerRepository, InMemoryLedgerRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RecordTransactionCommandHandler).Assembly));

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

public partial class Program { }