using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Negocio.Dto
{
       public class ProductoDto
        {
            public Guid Id { get; set; }
            [Required]
            [MinLength(1,
            ErrorMessage = "Nombre de Producto Requerido")]
            public string Nombre { get; set; }
            [Required]
            [MinLength(1,
            ErrorMessage = "Codigo de Barras Requerido")]
            public string CodigoBarras { get; set; }
            [Required]
            [Range(0,100000000, 
            ErrorMessage = "Precio Requerido")]
            public decimal Precio { get; set; }

        }
}
