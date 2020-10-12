using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Negocio.Dto
{
        public class ExistenciaDto
        {
            public Guid IdSucursal { get; set; }
            public Guid IdProducto { get; set; }
            [Range(0, 1000000,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
            public int Cantidad { get; set; }
        }
    
}
