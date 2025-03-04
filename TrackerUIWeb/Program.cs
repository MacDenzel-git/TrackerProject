using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using TrackerUIWeb;
using TrackerUIWeb.Data;
using TrackerUIWeb.Data.ApiGateway;
using TrackerUIWeb.Data.AuthenticationHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
 builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazorBootstrap();
builder.Services.AddServices();
builder.Services.AddScoped(xp => new HttpClient { BaseAddress = new Uri(builder.Configuration["EndPointUrl"] ?? "http://0.0.0.0") });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(options =>
   {
       options.Cookie.Name = "Auth_token";
       options.LoginPath = "/Login";
       options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
       options.AccessDeniedPath = "/access-denied";

   });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

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
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
