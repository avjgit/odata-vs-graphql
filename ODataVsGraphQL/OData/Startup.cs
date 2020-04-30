using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OData.Models;

namespace OData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);

            builder.EntitySet<Course>("Courses");


            app.UseMvc(routeBuilder =>
            {
                // pieejamo iespēju konfigurācija
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());
            });
        }
    }
}
