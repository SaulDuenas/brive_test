using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.API.Context;
using StoreApp.Negocio.Model;
using StoreApp.Negocio.Dto;
using AutoMapper;
using StoreApp.Negocio.Repositorio;


namespace Store.API.Repositorio
{
    public class RepositorioSucursal : IRepositorioSucursal, IDisposable
    {
        private readonly StoreAppContext _context;
        private readonly IMapper _mapper;

        public RepositorioSucursal(IMapper mapper,StoreAppContext context)
        {

            _mapper = mapper;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }


        public async Task<IEnumerable<SucursalDto>> ObtenerTiendasAsync()
        {

            return await _context.Sucursal.Select(_mapper.Map<Sucursal, SucursalDto>)
                                          .OrderBy(c => c.Nombre)
                                          .ToAsyncEnumerable().ToList();// OrderBy(c => c.Nombre)
           
        }

        public async Task<SucursalDto> ObtenerTienda(Guid id)
        {
            return await _context.Sucursal.Where(f => f.Id == id).
                                Select(_mapper.Map<Sucursal, SucursalDto>).
                                ToAsyncEnumerable().FirstOrDefault();
        }


        public async Task<SucursalCustomDto> ObtenerTiendaConInventario(Guid id)
        {
            var sucursal =  await _context.Sucursal.Where(f => f.Id == id).Select(_mapper.Map<Sucursal, SucursalCustomDto>).
                                                    ToAsyncEnumerable().FirstOrDefault();
            if (!(sucursal == null))
            {
                sucursal.Existencias = _context.Existencia.Where(f => f.IdSucursal == id)
                                                           .Select(_mapper.Map<Existencia, ExistenciaCustomDto>)
                                                           .ToList();
            }

            return sucursal;
        }


        public async Task<SucursalDto> ObtenerTienda(string Nombre)
        {
            return await _context.Sucursal.Where(f => f.Nombre.ToString().Contains(Nombre)).
                                     Select(_mapper.Map<Sucursal, SucursalDto>).
                                     ToAsyncEnumerable().FirstOrDefault();
        }



        public async Task<bool> ExisteNombreTienda(string name)
        {
            return await _context.Sucursal.ToAsyncEnumerable().Any(c => c.Nombre == name);
        }

        public async Task<SucursalDto> AgregarTienda(SucursalDto sucursal)
        {
            var NewStore = new Sucursal() { Id = Guid.Empty,Nombre = sucursal.Nombre };
            var store =   _context.Sucursal.Add(NewStore);
            await _context.SaveChangesAsync();

            return _mapper.Map <Sucursal, SucursalDto >(NewStore);
        }

    }
}
