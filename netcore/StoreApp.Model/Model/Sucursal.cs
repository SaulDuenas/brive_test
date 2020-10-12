using System;
using System.Collections.Generic;

namespace StoreApp.Negocio.Model
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Existencia = new HashSet<Existencia>();
            Movimiento = new HashSet<Movimiento>();
        }

        public Guid Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Existencia> Existencia { get; set; }
        public virtual ICollection<Movimiento> Movimiento { get; set; }
    }
}
