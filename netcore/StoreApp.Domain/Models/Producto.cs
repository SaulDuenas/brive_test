using System;
using System.Collections.Generic;

namespace StoreApp.Domain.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Inventario = new HashSet<Inventario>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Codigobarras { get; set; }
        public decimal Precio { get; set; }

        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
