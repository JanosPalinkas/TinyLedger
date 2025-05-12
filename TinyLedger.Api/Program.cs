using MediatR;
using TinyLedger.Application.UseCases.Transactions;
using TinyLedger.Domain;
using TinyLedger.Infrastructure;
using TinyLedger.Infrastructure.Persistence;
using TinyLedger.Infrastructure.Repositories;
using TinyLedger.Api.Middlewares;
using TinyLedger.Api.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TinyLedgerDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddSingleton<ILedgerRepository, InMemoryLedgerRepository>();
builder.Services.AddScoped<ILedgerRepository, EfCoreLedgerRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RecordTransactionCommandHandler).Assembly));

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new SafeEnumConverter<TransactionType>());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();

public partial class Program { }
