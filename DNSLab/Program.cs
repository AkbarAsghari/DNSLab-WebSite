using Blazored.LocalStorage;
using DNSLab.Data;
using DNSLab.Helper.Exceptions;
using DNSLab.Helper.Extensions;
using DNSLab.Helper.HttpService;
using DNSLab.Interfaces.Auth;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using DNSLab.Prividers;
using DNSLab.Repository;
using DNSLab.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//ToastService
builder.Services.AddScoped<ToastService>();
//Exception Handlerer

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDNSRepository, DNSRepository>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthService>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());


builder.Services.AddLocalization();

builder.Services.AddSingleton<ApplicationExceptions>();
builder.Services.AddTransient<HttpResponseExceptionHander>();
// Add services to the container.
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddHttpContextAccessor();

var host = builder.Build();


var supportedCultures = new[] { "fa-FA", "en-EN" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

host.UseRequestLocalization(localizationOptions);

host.MapControllers();

// Configure the HTTP request pipeline.
if (!host.Environment.IsDevelopment())
{
    host.UseExceptionHandler("/Error");
}


host.UseStaticFiles();

host.UseRouting();

host.MapBlazorHub();
host.MapFallbackToPage("/_Host");

await host.RunAsync();
