using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using WhoIsMyGDaddy.API.Domain.Persistence.Contexts;
using WhoIsMyGDaddy.API.Domain.Repositories;
using WhoIsMyGDaddy.API.Domain.Services;
using WhoIsMyGDaddy.API.Persistence.Repositories;
using WhoIsMyGDaddy.API.Services;

namespace WhoIsMyGDaddy.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200");
                });
            });
            services.AddControllers();


            if (CurrentEnvironment.IsEnvironment("Testing")) {

                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
                

            } else {
                services.AddDbContext<AppDbContext>(options => {
                // options.UseInMemoryDatabase("whos-my-g-daddy-api-in-memory");
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
            });
            }

            // services.AddDbContext<AppDbContext>(options =>
            //             options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")),
            // ServiceLifetime.Transient);

           

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(MyAllowSpecificOrigins); 

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
