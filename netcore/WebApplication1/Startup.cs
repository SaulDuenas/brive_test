using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumeWebApi.Proxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WebApplication1.Repositorio;

namespace WebApplication1
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ??
               throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

          
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IRepositorioSucursal, RepositorioSucursal>();
            services.AddScoped<IRepositorioProducto, RepositorioProducto>();


            var baseurl = _configuration["ConnectionWebApi:baseUrl"];
            var encode = _configuration["ConnectionWebApi:encode"];


            services.AddSingleton<IProxy>(new Proxy(baseurl, encode));
          
            //services.AddScoped<IRepositorioExistencia, RepositorioExistencia>();
            //services.AddScoped<IRepositorioProducto, RepositorioProducto>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
