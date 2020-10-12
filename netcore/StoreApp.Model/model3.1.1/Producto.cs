using System;
using System.Collections.Generic;

namespace StoreApp.Model
{
    public partial class Producto
    {
        public Producto()
        {
            Inventario = new HashSet<Inventario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigobarras { get; set; }
        public decimal Precio { get; set; }

        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
