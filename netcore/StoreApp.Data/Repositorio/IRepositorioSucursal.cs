using StoreApp.Negocio;
using StoreApp.Negocio.Dto;
using StoreApp.Negocio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Negocio.Repositorio
{
    public interface IRepositorioSucursal
    {
        Task<IEnumerable<SucursalDto>> ObtenerTiendasAsync();
        Task<SucursalDto> ObtenerTienda(Guid id);
        Task<SucursalDto> ObtenerTienda(string name);
        Task<SucursalCustomDto> ObtenerTiendaConInventario(Guid id);
        Task<SucursalDto> AgregarTienda(SucursalDto sucursal);
        Task<bool> ExisteNombreTienda(string name);
    }
}
