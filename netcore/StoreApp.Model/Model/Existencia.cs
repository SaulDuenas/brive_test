using System;
using System.Collections.Generic;

namespace StoreApp.Negocio.Model
{
    public partial class Existencia
    {
        public Guid IdSucursal { get; set; }
        public Guid IdProducto { get; set; }
        public int Cantidad { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
        public virtual Sucursal IdsucursalNavigation { get; set; }
    }
}
