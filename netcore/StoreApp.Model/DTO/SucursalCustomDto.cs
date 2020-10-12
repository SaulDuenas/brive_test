using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Negocio.Dto
{
    public partial class SucursalCustomDto: SucursalDto
    {
        public virtual IEnumerable<ExistenciaCustomDto> Existencias { get; set; }
    }
}
