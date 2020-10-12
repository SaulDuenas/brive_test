using StoreApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API.Repository
{
    public interface IStoreRepository
    {
        IEnumerable<Sucursal> ObtenerTiendas();

        Sucursal ObtenerTienda(Guid id);

        Sucursal ObtenerTienda(string name);

        void AgregarTienda(SucursalDto sucursal);

        bool ExisteNombreTienda(string name);


    }
}
