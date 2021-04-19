using AutoMapper;
using DFC.App.Lmi.Delta.Report.Connectors;
using DFC.App.Lmi.Delta.Report.Contracts;
using DFC.App.Lmi.Delta.Report.Extensions;
using DFC.App.Lmi.Delta.Report.HttpClientPolicies;
using DFC.App.Lmi.Delta.Report.Models;
using DFC.App.Lmi.Delta.Report.Models.ClientOptions;
using DFC.App.Lmi.Delta.Report.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.Lmi.Delta.Report
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string AppSettingsPolicies = "Policies";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddHttpClient();
            services.AddTransient<IApiConnector, ApiConnector>();
            services.AddTransient<IApiDataConnector, ApiDataConnector>();
            services.AddTransient<IDeltaReportService, DeltaReportService>();
            services.AddSingleton(new CachedDeltaReportModel());

            var policyOptions = Configuration.GetSection(AppSettingsPolicies).Get<PolicyOptions>() ?? new PolicyOptions();
            var policyRegistry = services.AddPolicyRegistry();

            services.AddSingleton(Configuration.GetSection(nameof(DeltaReportApiClientOptions)).Get<DeltaReportApiClientOptions>() ?? new DeltaReportApiClientOptions());

            services
                .AddPolicies(policyRegistry, nameof(DeltaReportApiClientOptions), policyOptions)
                .AddHttpClient<IDeltaReportApiConnector, DeltaReportApiConnector, DeltaReportApiClientOptions>(nameof(DeltaReportApiClientOptions), nameof(PolicyOptions.HttpRetry), nameof(PolicyOptions.HttpCircuitBreaker));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=DeltaReport}/{action=Index}/{id?}");
            });
        }
    }
}
