using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SampleUniversity.Data;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using SampleUniversity.Model;

namespace SampleUniversity
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

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

            //var builder = new ODataConventionModelBuilder(app.ApplicationServices);

            //builder.EntitySet<Student>("Students");
        }
    }
}
