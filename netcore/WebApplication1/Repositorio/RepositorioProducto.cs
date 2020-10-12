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
    public class RepositorioProducto : IRepositorioProducto
    {
        private readonly IProxy _proxy;

        public RepositorioProducto(IProxy proxy)
        {
            _proxy = proxy;
        }



        public async Task<bool> ExisteNombreProducto(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ProxyResponse<ProductoDto>> ObtenerProducto(Guid id)
        {
            throw new NotImplementedException();
        }



        public async Task<ProxyResponse<IEnumerable<ProductoDto>>> ObtenerProductosAsync()
        {
            //ProxyResponse<IEnumerable<SucursalDto>> Resp = await _proxy.GetMessageAsync<SucursalDto>("tiendas");

            var Resp = await _proxy.GetMessageAsync<IEnumerable<ProductoDto>>("productos");

            return Resp;
        }


        public async Task<ProxyResponse<ProductoDto>> AgregarProducto(ProductoDto producto)
        {
            var response = await _proxy.PostMessageAsync<ProductoDto>("producto/agregar", producto);

            return response;

        }

    }
}

