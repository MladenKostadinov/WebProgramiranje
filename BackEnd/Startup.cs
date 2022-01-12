using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

namespace Projekat
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); // razor auto update
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projekat", Version = "v1" });
            });
            services.AddDbContext<BibliotekaContext>(p => p.UseSqlServer(Configuration.GetConnectionString("BibliotekaString"))); //connection string


            services.AddCors(options =>                                                     //
            {                                                                               //      cors
                options.AddPolicy(name: "Policy",                                           //
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:5501")
                        .AllowAnyHeader()                                                   //
                        .AllowAnyMethod();                                                  //
                    });                                                                     //
            });                                                                             //
            services.AddMvc(); //razor pages
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projekat v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Policy");       //cors

            app.UseAuthorization();

            app.UseStaticFiles(); // razor pages

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();//razor pages
            });
        }
    }
}
