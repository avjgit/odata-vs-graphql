using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SampleUniversity.Data;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using SampleUniversity.Model;

namespace SampleUniversity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContext<UniversityContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("UniversityContext")));

            services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson();

            services.AddOData();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Expand().Select().OrderBy().Filter();
            });

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);

            builder.EntitySet<Student>("Students");

            //app.UseMvc(routeBuilder =>
            //{
            //    routeBuilder.EnableDependencyInjection();

            //    // and this line to enable OData query option, for example $filter

            //    routeBuilder.Expand().Select().OrderBy().Filter();

            //    routeBuilder.MapODataServiceRoute("ODataRoute", "api", builder.GetEdmModel());

            //});
        }
    }
}
