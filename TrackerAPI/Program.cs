using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrackerAPI;
using BusinessLogicLayer.Logging;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.DataTransferObjects;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddRepositories();

//add Auth
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

 
builder.Services.AddDbContext<TrackerDbContext>(
options => {
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentityCore<SystemUser>()
        .AddEntityFrameworkStores<TrackerDbContext>()
        .AddApiEndpoints();

 
builder.Host.ConfigureLogging((context, logging) =>
{
    logging.AddRoundCodeFileLogger(options =>
    {
        context.Configuration.GetSection("RoundTheCodeFile").GetSection("Options").Bind(options);
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapIdentityApi<SystemUser>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
