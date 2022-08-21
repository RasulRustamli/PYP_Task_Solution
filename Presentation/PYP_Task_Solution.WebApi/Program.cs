using PYP_Task_Solution.Aplication;
using PYP_Task_Solution.Infrastructure;
using PYP_Task_Solution.Persistence;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using PYP_Task_Solution.Aplication.Features.Queries;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAplicationServices();


var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn
            {ColumnName = "RapartType", PropertyName = "RapartType", DataType = SqlDbType.NVarChar},
        new SqlColumn
            {ColumnName = "Email",PropertyName = "Email", DataType = SqlDbType.NVarChar},
        new SqlColumn
            {ColumnName = "SendRaportDate",PropertyName = "SendRaportDate", DataType = SqlDbType.DateTime},
    }
};

Log.Logger = new LoggerConfiguration()
   .WriteTo.File("logs/logs.txt")
   .WriteTo.Console()
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("Default"),
                           "logs",
                           autoCreateSqlTable: true,
                           restrictedToMinimumLevel: LogEventLevel.Verbose,
                           columnOptions: columnOptions
                           )
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog();



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ReportQueryHandler>());




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
