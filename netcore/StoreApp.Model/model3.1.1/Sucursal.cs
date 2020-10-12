using System;
using System.Collections.Generic;

namespace StoreApp.Model
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Inventario = new HashSet<Inventario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
