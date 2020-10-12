using System;
using System.Collections.Generic;

namespace StoreApp.Negocio.Model
{
    public partial class Producto
    {
        public Producto()
        {
            Existencia = new HashSet<Existencia>();
            Movimiento = new HashSet<Movimiento>();
        }

        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarras { get; set; }
        public decimal? Precio { get; set; }

        public virtual ICollection<Existencia> Existencia { get; set; }
        public virtual ICollection<Movimiento> Movimiento { get; set; }
    }
}
