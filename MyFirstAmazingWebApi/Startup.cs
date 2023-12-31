using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyFirstAmazingWebApi.Middlewares;

namespace MyFirstAmazingWebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MapWhen", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("", "MyFirstAmazingWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware Use Started");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware Use Ended");
            //});

            //app.Run(async context => Console.WriteLine("Middleware Run"));

            //app.Map("/home", builder =>
            //{
            //    builder.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("Middleware Map Started");
            //        await next.Invoke();
            //        Console.WriteLine("Middleware Map Ended");
            //    });
            //});

            //app.UseHakuna();

            app.MapWhen(context => {

                Console.WriteLine("MapWhen Started");
                return true;

            }, HandleId);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Good bye, World...");
            });

            app.UseMiddleware<HakunaMiddleware>();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void HandleId(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("(....MapWhen Used....)");
            });
        }
    }
}
