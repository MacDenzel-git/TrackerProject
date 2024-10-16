using Blazored.SessionStorage;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TrackerUIWeb;
using TrackerUIWeb.Data;
using TrackerUIWeb.Data.ApiGateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
 builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazorBootstrap();
builder.Services.AddServices();
builder.Services.AddScoped(xp => new HttpClient { BaseAddress = new Uri(builder.Configuration["EndPointUrl"] ?? "http://0.0.0.0") });
 





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
