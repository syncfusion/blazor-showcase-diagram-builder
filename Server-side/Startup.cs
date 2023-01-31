using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using DiagramBuilder.Shared;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace DiagramBuilder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
#pragma warning disable CA1822 // Mark members as static
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            services.AddSignalR(e =>
            {
                e.MaximumReceiveMessageSize = 102400000;
            });
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhjVFpFaV1AQmFJfFBmQGlaelRwfUU3HVdTRHRdQ19hQX5XdkxmUH5acHI=;Mgo+DSMBPh8sVXJ0S0J+XE9HflRDQmFLYVF2R2BJelRzcV9GaUwgOX1dQl9hSHxRfkVnWXZbeXVQQGY=;ORg4AjUWIQA/Gnt2VVhkQlFadVdJXHxLfEx0RWFab1t6d1BMYFVBNQtUQF1hS39RdE1jXX9XdX1UQWNa;ODI4NTE2QDMyMzAyZTM0MmUzMFo1amQ0SDBicFM2QUh1c3lUQ29QM0duUndhdWNnTis5TkZ6WVErOGpQOU09;ODI4NTE3QDMyMzAyZTM0MmUzMGI4SlRxNmtMeW9zeEdyaU54allqeGRKSndlczFQU2pZalpUZ3JYaThGaE09;NRAiBiAaIQQuGjN/V0Z+WE9EaFxKVmJWfFFpR2NbfE51flZDalxYVAciSV9jS3xSdEdrWXteeXFcRGRdVw==;ODI4NTE5QDMyMzAyZTM0MmUzMExKU29GQnlpanZvNDFHUWxGcU13RGxGMURyQ013dnNoU3hENFpReW1SWlk9;ODI4NTIwQDMyMzAyZTM0MmUzMFRGMi82cjBLa3lLanZ2MkloRVVYdE0zdkxHYTJkREJuZExyVXFidzR0OVk9;Mgo+DSMBMAY9C3t2VVhkQlFadVdJXHxLfEx0RWFab1t6d1BMYFVBNQtUQF1hS39RdE1jXX9XdX1XT2la;ODI4NTIyQDMyMzAyZTM0MmUzMGNrZlA2RDdCN2MybUNIMFNpK3hlTHBYKzhjZ3BMVElsQkh1aUgwOHdRZjg9;ODI4NTIzQDMyMzAyZTM0MmUzMEptNm54a3p4WXp4QTU0RkVWRm8xRGdqRVJpRnhvcFY0ZHhIU244aTlSck09;ODI4NTI0QDMyMzAyZTM0MmUzMExKU29GQnlpanZvNDFHUWxGcU13RGxGMURyQ013dnNoU3hENFpReW1SWlk9");
            services.AddSyncfusionBlazor();
            services.AddScoped<SampleService>();
            // Register the Syncfusion locale service to customize the SyncfusionBlazor component locale culture
            //services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
            services.Configure<RequestLocalizationOptions>(options =>
            {
                // Define the list of cultures your app will support
                List<CultureInfo> supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de"),
                    new CultureInfo("fr"),
                    new CultureInfo("ar"),
                    new CultureInfo("zh"),
                };
                // Set the default culture
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CA1822 // Mark members as static
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CA1822 // Mark members as static
        {
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
