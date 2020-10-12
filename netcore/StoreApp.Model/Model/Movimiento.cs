using System;
using System.Collections.Generic;

namespace StoreApp.Negocio.Model
{
    public partial class Movimiento
    {
        public Guid Id { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdProducto { get; set; }
        public int IdTipoTrans { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioOperacion { get; set; }
        public DateTime? FechaOperacion { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
        public virtual Sucursal IdsucursalNavigation { get; set; }
        public virtual Tipotransaccion IdtipotransNavigation { get; set; }
    }
}
