using Blazored.LocalStorage;
using DNSLab.Components;
using DNSLab.Helper.Exceptions;
using DNSLab.Helper.HttpService;
using DNSLab.Interfaces.Auth;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using DNSLab.Prividers;
using DNSLab.Repository;
using DNSLab.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using System.IO.Compression;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace DNSLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

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
            builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddScoped<JWTAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
            builder.Services.AddScoped<IAuthService>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

            builder.Services.AddTransient<IMetadataProvider, MetadataProvider>();
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

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            var supportedCultures = new[] { "fa-FA", "en-EN" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
