using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp;

namespace Store.API.Repository
{
    public class SucursalRepositorio : IStoreRepository, IDisposable
    {
        private readonly StoreAppContext _context;

        public SucursalRepositorio(StoreAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }


        public IEnumerable<Sucursal> ObtenerTiendas()
        {
            return _context.Sucursal.OrderBy(c => c.Nombre).ToList();
        }

        public Sucursal ObtenerTienda(Guid id)
        {
            return _context.Sucursal.Where(f => f.Id == id).OrderBy(c => c.Nombre).FirstOrDefault();
        }        

        public Sucursal ObtenerTienda(string Nombre)
        {
            return _context.Sucursal.Where(f => f.Nombre.ToString().Contains(Nombre)).OrderBy(c => c.Nombre).FirstOrDefault();
        }

        public bool ExisteNombreTienda(string name)
        {
            return _context.Sucursal.Any(c => c.Nombre == name);
        }

        public void AgregarTienda(SucursalDto sucursal)
        {
            var store = _context.Sucursal.Add(new Sucursal() { Nombre = sucursal.Nombre });
            _context.SaveChanges();

        }

    }
}
