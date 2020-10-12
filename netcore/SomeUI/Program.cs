using StoreApp.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeUI
{
    class Program: IDisposable
    {
        private static StoreAppContext _dbcontext = new StoreAppContext();
        static void Main(string[] args)
        {
            Console.WriteLine("EF Core - Inicios");

            var productos = new List<Producto>();
            var inventarios = new List<Existencia>();
            var sucursales = new List<Sucursal>();

            //using (var dbcontext = new StoreAppContext())
            //{
            //    productos = dbcontext.Producto.ToList();
            //    inventarios = dbcontext.Inventario.ToList();
            //    sucursales = dbcontext.Sucursal.ToList();

            //}

            productos = _dbcontext.Producto.ToList();
            inventarios = _dbcontext.Inventario.ToList();
            sucursales = _dbcontext.Sucursal.ToList();


            var query = (from inv in inventarios
                         join prod in productos on inv.IdProducto equals prod.Id
                         join suc in sucursales on inv.IdSucursal equals suc.Id
                         select new
                         {
                             NombreSuc = suc.Nombre,
                             NombreProd = prod.Nombre,
                             prod.CodigoBarras,
                             prod.Precio,
                             inv.Cantidad
                         }).ToList();

            //foreach (var rw in query) {

            //    Console.WriteLine(rw.NombreSuc + "|"+rw.NombreProd + "|" + rw.Codigobarras + "|"+rw.Precio + "|" + rw.Cantidad);

            //}

            query.ForEach(rw => Console.WriteLine(rw.NombreSuc + "|" + rw.NombreProd + "|" + rw.CodigoBarras + "|" + rw.Precio + "|" + rw.Cantidad));

            Console.ReadLine();

            //cargaInicial();

        }


        static void cargaInicial() {

            var tiendas = new List<Sucursal>();


            tiendas.AddRange(new List<Sucursal> { new Sucursal() { Nombre = "Sucursal A" },
                                                      new Sucursal() { Nombre = "Sucursal B" } });

            var productos = new List<Producto>();

            productos.AddRange(new List<Producto> { new Producto() { Nombre="Café legal",CodigoBarras="100010",Precio=new Decimal(7.0)},
                                                        new Producto() { Nombre="Chocolate Abuelita",CodigoBarras="100011",Precio=new Decimal(15.0)},
                                                        new Producto() { Nombre="Bonafina",CodigoBarras="100012",Precio=new Decimal(12.0)}
                                                      });

            using (var dbcontext = new StoreAppContext())
            {
               
                dbcontext.Sucursal.AddRange(tiendas);
                dbcontext.Producto.AddRange(productos);

                dbcontext.SaveChanges();
            }

        }

        public void Dispose()
        {
            _dbcontext.Dispose();
            //throw new NotImplementedException();
        }
    }
}
