using System;
using System.Collections.Generic;

namespace StoreApp.Negocio.Dto
{
    public class ExistenciaCustomDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal ?Precio { get; set; }
    }
}
