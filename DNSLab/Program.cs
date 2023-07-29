using Blazored.LocalStorage;
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
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;
using System.Globalization;
using System.IO.Compression;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDNSRepository, DNSRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IIPRepository, IPRepository>();
builder.Services.AddScoped<IDNSLookUpRepository, DNSLookUpRepository>();
builder.Services.AddScoped<IStaticsRepository, StaticsRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); ;
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthService>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

builder.Services.AddTransient<MetadataProvider>();
builder.Services.AddScoped<MetadataTransferService>();

builder.Services.AddSingleton<HtmlEncoder>(
  HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                                            UnicodeRanges.Arabic }));

builder.Services.AddLocalization();

builder.Services.AddSingleton<ApplicationExceptions>();
builder.Services.AddTransient<HttpResponseExceptionHander>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR(e =>
{
    e.MaximumReceiveMessageSize = 3145728; //3MB
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

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

host.UseResponseCompression();

await host.RunAsync();
