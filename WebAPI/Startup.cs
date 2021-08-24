using EntityFrameworkCore.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters.ResourceFilterVersionMangement;

namespace WebAPIRouting
{
    public class Startup
    {
        //set dev environment
        private readonly IWebHostEnvironment env;

        public Startup(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            if (env.IsDevelopment())
            {
                //cuz we need to create MemoryDatabase every it starts
               
                services.AddDbContext<TicketingSystemContext>(options =>
                {
                    options.UseInMemoryDatabase("TicketingSystem");
                }
                );
            }


            //   services.AddControllers();



            //apply this to set filter global
            services.AddControllers(options =>
            options.Filters.Add<Version1Expired>()
            ) ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //we can add more params on ConfigureService
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TicketingSystemContext context)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureDeleted();//just in case
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
