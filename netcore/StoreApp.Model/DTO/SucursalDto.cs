using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Negocio.Dto
{
     public partial class SucursalDto
        {
            public Guid Id { get; set; }
            [Required]
            [MinLength(1, ErrorMessage = "Nombre de Sucursal Requerido")]
            public string Nombre { get; set; }
        }

}
