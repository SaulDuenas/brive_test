using System;
using System.Collections.Generic;

namespace StoreApp.Negocio.Dto
{
    public partial class MovimientoDto
    {
        public Guid Id { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdProducto { get; set; }
        public int IdTipoTrans { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioOperacion { get; set; }
        public DateTime? FechaOperacion { get; set; }

    }
}
