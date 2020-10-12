using ConsumeWebApi.Proxy;
using StoreApp.Negocio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Repositorio
{
    public class RepositorioSucursal : IRepositorioSucursal
    {
        private readonly IProxy _proxy;

        public RepositorioSucursal(IProxy proxy)
        {
            _proxy = proxy;
        }

     

        public async Task<bool> ExisteNombreTienda(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ProxyResponse<SucursalDto>> ObtenerTienda(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProxyResponse<SucursalDto>> ObtenerTienda(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ProxyResponse<SucursalCustomDto>> ObtenerTiendaConInventario(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProxyResponse<IEnumerable<SucursalDto>>> ObtenerTiendasAsync()
        {
            //ProxyResponse<IEnumerable<SucursalDto>> Resp = await _proxy.GetMessageAsync<SucursalDto>("tiendas");

            var Resp = await _proxy.GetMessageAsync<IEnumerable<SucursalDto>>("tiendas");

            return Resp;
        }


        public async Task<ProxyResponse<SucursalDto>> AgregarTienda(SucursalDto sucursal)
        {
          
            var response = await _proxy.PostMessageAsync<SucursalDto>("tienda/nuevo", sucursal);

            return response;
        }

    }
}
