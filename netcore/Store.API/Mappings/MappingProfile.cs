using AutoMapper;
using StoreApp.Negocio;
using StoreApp.Negocio.Dto;
using StoreApp.Negocio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Existencia, ExistenciaDto>();
            CreateMap<Producto, ProductoDto>();
            CreateMap<Sucursal, SucursalDto>();
            CreateMap<Sucursal, SucursalCustomDto>();
        }
    }
 
}
