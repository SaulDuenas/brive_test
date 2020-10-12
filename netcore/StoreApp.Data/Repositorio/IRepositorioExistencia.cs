using StoreApp.Negocio.Dto;
using StoreApp.Negocio.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.Negocio.Repositorio
{
    public interface IRepositorioExistencia
    {
        Task<ICollection<Existencia>> ObtenerExistencias(Guid IdSucursal);
        Task<IEnumerable<ExistenciaCustomDto>> ObtenerExistenciasCustom(Guid IdSucursal);
        Task<ExistenciaCustomDto> ObtenerExistenciaCustom(Guid tiendaId, Guid productoId);
        Task<bool> ActualizarExistencia(Existencia exsitencia, Movimiento movimiento);

    }
}