using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using StoreApp.Negocio.catalogos;
using Store.API.Context;
using StoreApp.Negocio.Model;
using StoreApp.Negocio.Dto;
using StoreApp.Negocio.Repositorio;
using System.Threading.Tasks;

namespace Store.API.Repositorio
{
    public class RepositorioExistencia : IRepositorioExistencia, IDisposable
    {
        private readonly StoreAppContext _context;

        public RepositorioExistencia(StoreAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ICollection<Existencia>> ObtenerExistencias(Guid Idsucursal)
        {
            return await _context.Existencia.Where(f => f.IdSucursal == Idsucursal).ToAsyncEnumerable().ToList();
        }

        public async Task<Existencia> ObtenerInventario(Guid Idsucursal, Guid IdProducto)
        {
            return await _context.Existencia.Where(f => (f.IdSucursal == Idsucursal && f.IdProducto == IdProducto))
                                 .ToAsyncEnumerable().FirstOrDefault();
        }


        public async Task<IEnumerable<ExistenciaCustomDto>> ObtenerExistenciasCustom(Guid tiendaId)
        {
           // _context.Existencia.Where(f => f.IdSucursal == tiendaId).ToList();

            var result = await _context.Existencia.Where(f => f.IdSucursal == tiendaId)
                                                 .Join(_context.Producto, E => E.IdProducto, P => P.Id, (E, P) => new ExistenciaCustomDto()
                                                      {
                                                         Precio = P.Precio,
                                                         Cantidad = E.Cantidad,
                                                         Nombre = P.Nombre.Trim(),
                                                         Id = E.IdProducto 
                                                      }).ToAsyncEnumerable().ToList();

            return result;
        }


        public async Task<ExistenciaCustomDto> ObtenerExistenciaCustom(Guid tiendaId,Guid productoId)
        {
           // _context.Existencia.Where(f => f.IdSucursal == tiendaId).ToList();

            var result = await _context.Existencia.Where(f => f.IdSucursal == tiendaId && f.IdProducto == productoId)
                                 .Join(_context.Producto, E => E.IdProducto, P => P.Id, (E, P) => new ExistenciaCustomDto()
                                 {
                                     Precio = P.Precio,
                                     Cantidad = E.Cantidad,
                                     Nombre = P.Nombre.Trim(),
                                     Id = E.IdProducto
                                 }).ToAsyncEnumerable().FirstOrDefault();

            return result;
        }



        public async Task<bool> ActualizarExistencia(Existencia exsitencia,Movimiento movimiento)
        {
            _context.Existencia.Where(f => f.IdProducto == exsitencia.IdProducto && f.IdSucursal == exsitencia.IdSucursal).FirstOrDefault().Cantidad = exsitencia.Cantidad;

            _context.Movimiento.Add(movimiento);
            // actualizar movimientos 

            return (await _context.SaveChangesAsync() >= 0);

        }

        public async Task<bool> ExisteInventario(ExistenciaDto inventario) {
            return await _context.Existencia.Where(f => (f.IdSucursal == inventario.IdSucursal && f.IdProducto == inventario.IdProducto))
                                            .ToAsyncEnumerable().Any();
        }

        public void Dispose()
        {
            if (!(_context == null)) _context.Dispose();
        }
    }
}
