// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Taarafo.Portal.Web.Brokers.Apis;
using Taarafo.Portal.Web.Brokers.DateTimes;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Services.Foundations.Authors;
using Taarafo.Portal.Web.Services.Foundations.Comments;
using Taarafo.Portal.Web.Services.Foundations.Posts;
using Taarafo.Portal.Web.Services.Views.CommentViews;
using Taarafo.Portal.Web.Services.Views.PostViews;

namespace Taarafo.Portal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            AddRootDirectory(services);
            services.AddLogging();
            services.AddHttpClient();
            AddBrokers(services);
            AddServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string syncFustionLicenseKey = Configuration["Syncfusion:LicenseKey"];
            SyncfusionLicenseProvider.RegisterLicense(syncFustionLicenseKey);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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

        private static void AddRootDirectory(IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.RootDirectory = "/Views/Pages";
            });
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddScoped<IApiBroker, ApiBroker>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
            services.AddScoped<IDateTimeBroker, DateTimeBroker>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostViewService, PostViewService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentViewService, CommentViewService>();
        }
    }
}