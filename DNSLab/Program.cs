using DNSLab.Data;
using DNSLab.Helper.Exceptions;
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
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//ToastService
builder.Services.AddScoped<ToastService>();
//Exception Handlerer
builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDNSRepository, DNSRepository>();
builder.Services.AddScoped<IIPRepository, IPRepository>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider=>provider.GetRequiredService<JWTAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthService>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());


builder.Services.AddSingleton<HttpClient>();

builder.Services.AddHttpClientInterceptor();

builder.Services.AddLocalization();

builder.Services.AddSingleton<ApplicationExceptions>();

// Add services to the container.
builder.Services.AddSingleton<WeatherForecastService>();

var host = builder.Build();

//builder.Services.AddLocalization();

//host.UseRequestLocalization("fa");

//host.UseRequestLocalization(new RequestLocalizationOptions()
//    .AddSupportedCultures(new[] { "en", "fa" })
//    .AddSupportedUICultures(new[] { "en", "fa" }));


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
