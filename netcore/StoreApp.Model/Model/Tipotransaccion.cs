using System;
using System.Collections.Generic;

namespace StoreApp.Negocio.Model
{
    public partial class Tipotransaccion
    {
        public Tipotransaccion()
        {
            Movimiento = new HashSet<Movimiento>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Movimiento> Movimiento { get; set; }
    }
}
