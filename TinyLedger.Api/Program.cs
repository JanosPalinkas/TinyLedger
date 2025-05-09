using MediatR;
using TinyLedger.Application.UseCases.Transactions;
using TinyLedger.Domain;
using TinyLedger.Infrastructure;
using TinyLedger.Api.Middlewares;
using TinyLedger.Api.Converters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILedgerRepository, InMemoryLedgerRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RecordTransactionCommandHandler).Assembly));

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new SafeEnumConverter<TransactionType>());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.Run();

public partial class Program { }
