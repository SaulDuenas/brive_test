using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Store.API.Context;
using StoreApp.Negocio.Model;
using StoreApp.Negocio.Repositorio;
using System.Threading.Tasks;

namespace Store.API.Repositorio
{
    public class RepositorioProducto : IDisposable, IRepositorioProducto
    {
        private readonly StoreAppContext _context;

        public RepositorioProducto(StoreAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }

        public async Task<IEnumerable<Producto>> ObtenerProductos()
        {
            return await _context.Producto.OrderBy(f => f.Id).ToAsyncEnumerable().ToList();
        }

        public async Task<Producto> ObtenerProducto(Guid IdProducto)
        {
            return await _context.Producto.Where(f => f.Id == IdProducto).ToAsyncEnumerable().FirstOrDefault();
        }

        public async Task<Producto> AgregarProducto(Producto producto)
        {
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            return producto;
        }


        public async Task<bool> ExistenombreProducto(string nombre) {

            return await _context.Producto.Where(f => f.Nombre.Trim() == nombre.Trim()).ToAsyncEnumerable().Any();
            
        }
    }
}
