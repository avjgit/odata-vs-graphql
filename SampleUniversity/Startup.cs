using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Extensions;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.AspNetCore;

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

            // pievieno MVC, atrisinot jaunākas Core versijas un OData maršrutēšanas problēmas
            services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson();

            // pievieno OData
            services.AddOData();

            // ieslēdz GraphQL
            services.AddGraphQL(
                SchemaBuilder.New()
                    .AddQueryType<GraphqlResolver>()
                    .Create(),
                new QueryExecutionOptions { ForceSerialExecution = true });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseGraphQL();
            app.UsePlayground();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            // ieslēdz OData iespējas
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                // tieši norāda, kādas OData iespējas pieejamas
                routeBuilder.Expand().Select().OrderBy().Filter(); 
            });


        }
    }
}