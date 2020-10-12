using StoreApp.Negocio.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.Negocio.Repositorio
{
    public interface IRepositorioProducto
    {
        Task<IEnumerable<Producto>> ObtenerProductos();
        Task<Producto> AgregarProducto(Producto producto);
        Task<Producto> ObtenerProducto(Guid IdProducto);
        Task<bool> ExistenombreProducto(string nombre);
    }
        
}