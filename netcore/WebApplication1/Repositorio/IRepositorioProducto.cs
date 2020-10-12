using ConsumeWebApi.Proxy;
using StoreApp.Negocio.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Repositorio
{
    public interface IRepositorioProducto
    {
        Task<ProxyResponse<ProductoDto>> AgregarProducto(ProductoDto producto);
        Task<bool> ExisteNombreProducto(string name);
        Task<ProxyResponse<ProductoDto>> ObtenerProducto(Guid id);
        Task<ProxyResponse<IEnumerable<ProductoDto>>> ObtenerProductosAsync();
    }
}