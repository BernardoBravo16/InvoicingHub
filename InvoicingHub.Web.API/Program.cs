using InvoicingHub.Common.Configurations;
using InvoicingHub.Persistence;
using InvoicingHub.Web.API.Shared.Initialize;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var InvoicingHubOrigins = "_InvoicingHubOrigins";

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(
    options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnectionString"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(InvoicingHubOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddPersistence();
builder.Services.AddApplication();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("DefaultConnectionString"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(InvoicingHubOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
