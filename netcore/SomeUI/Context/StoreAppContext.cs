using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoreApp.Negocio;


namespace SomeUI
{
    public partial class StoreAppContext : DbContext
    {
        string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StoreApp;Integrated Security=True;";

        public StoreAppContext()
        {

        }


        public StoreAppContext(DbContextOptions<StoreAppContext> options)
            : base(options)
        {

        }
       
        private ILoggerFactory GetLoggerFactory()
        {

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        public virtual DbSet<Existencia> Inventario { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(GetLoggerFactory())
                    .UseSqlServer(connectionstring);
            }
        }

     //   base.OnModelCreating(modelBuilder);
    }
}
