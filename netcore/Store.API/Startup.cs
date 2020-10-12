using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;

using StoreApp.Negocio.Repositorio;
using Store.API.Context;
using Store.API.Repositorio;

namespace Store.API
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
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
          
            services.AddMvc()
                    .AddMvcOptions(o =>
                     {
                         o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                        
                     });

            //.AddJsonOptions(
            //  o =>
            //  {
            //      if (o.SerializerSettings.ContractResolver != null)
            //      {
            //          var castresolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //      }
            //  }

            //  );
            
            var mappingConfig = new MapperConfiguration(mc => 
            {
               
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var connectionstring = _configuration["ConnectiosStrings:StoreAppConnectionString"];
            services.AddDbContext<StoreAppContext>(o => 
            {
                o.UseSqlServer(connectionstring);
            });

            services.AddScoped<IRepositorioSucursal, RepositorioSucursal>();
            services.AddScoped<IRepositorioExistencia, RepositorioExistencia>();
            services.AddScoped<IRepositorioProducto,RepositorioProducto>();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            
            if(env.IsProduction())  {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
