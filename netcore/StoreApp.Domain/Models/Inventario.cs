using System;
using System.Collections.Generic;

namespace StoreApp.Domain.Models
{
    public partial class Inventario
    {
        public int Id { get; set; }
        public int Idsucursal { get; set; }
        public int Idproducto { get; set; }
        public int Cantidad { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
        public virtual Sucursal IdsucursalNavigation { get; set; }
    }
}
