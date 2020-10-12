using System;
using System.Collections.Generic;

namespace StoreApp.Domain.Models
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Inventario = new HashSet<Inventario>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
