using ConsumeWebApi.Proxy;
using StoreApp.Negocio.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Repositorio
{
    public interface IRepositorioSucursal
    {
        Task<ProxyResponse<SucursalDto>> AgregarTienda(SucursalDto sucursal);
        Task<bool> ExisteNombreTienda(string name);
        Task<ProxyResponse<SucursalDto>> ObtenerTienda(Guid id);
        Task<ProxyResponse<SucursalDto>> ObtenerTienda(string name);
        Task<ProxyResponse<SucursalCustomDto>> ObtenerTiendaConInventario(Guid id);
        Task<ProxyResponse<IEnumerable<SucursalDto>>> ObtenerTiendasAsync();
    }
}