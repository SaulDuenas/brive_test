using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using StoreApp.Negocio.Repositorio;
using StoreApp.Negocio.catalogos;
using StoreApp.Negocio.Dto;
using StoreApp.Negocio.Model;


namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/empresa")]
    public class ControladorSucursales : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRepositorioSucursal _RepositorioSucursal;
        private readonly IRepositorioExistencia _RepositorioExistencia;

        // Assign the object in the constructor for dependency injection
        public ControladorSucursales(IMapper mapper, IRepositorioSucursal RepositorioTienda, IRepositorioExistencia RepositorioExistencia)
        {
            _mapper = mapper;
            _RepositorioSucursal = RepositorioTienda;
            _RepositorioExistencia = RepositorioExistencia;
        }


        [HttpGet("tiendas", Name = "GetStores")]
        public async Task<IActionResult> GetStores()
        {
            try
            {
                var sucursales = await _RepositorioSucursal.ObtenerTiendasAsync();

                if (sucursales != null && sucursales.Count() > 0)
                {
                    // var storeresult =   _mapper.Map<SucursalDto>(store);
                    return Ok(sucursales);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                // SDB: Manejo de log
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();

        }

        [HttpGet("tienda/{IdSucursal}", Name = "GetStore")]
        public async Task<IActionResult> GetStore(Guid IdSucursal)
        {
          try
            {
                var sucursal = await _RepositorioSucursal.ObtenerTienda(IdSucursal);

                if (sucursal != null)
                {
                    // var storeresult =   _mapper.Map<SucursalDto>(store);
                    return Ok(sucursal);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                // SDB: Manejo de log
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();

        }


        [HttpGet("tienda/{IdSucursal}/existencias", Name = "GetStorewithStock")]
        public async Task<IActionResult> GetStorewithStock(Guid IdSucursal)
        {
            try
            {
                var sucursal = await _RepositorioSucursal.ObtenerTiendaConInventario(IdSucursal);

                if (sucursal != null)
                {
                    return Ok(sucursal);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                // SDB: Manejo de log
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();

        }



        [HttpPost("tienda/nuevo")]
        public async Task<IActionResult> CreateNewStore([FromForm] SucursalDto sucursal) {
          
            if (await _RepositorioSucursal.ExisteNombreTienda(sucursal.Nombre.Trim()))
            {

                ModelState.AddModelError("Descripción",
                                         "El nombre de sucursal ya esta en uso");
            }
            else if (!(sucursal.Nombre.Trim().Length > 0)) {
                ModelState.AddModelError("Descripción",
                                         "Nombre de sucursal requerido");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tiendacreada = await _RepositorioSucursal.AgregarTienda(sucursal);

            return CreatedAtRoute("GetStore", new {IdSucursal = tiendacreada.Id }, null);
        }


        [HttpPut("tienda/{IdSucursal}/existencia/comprar")]
        public async Task<IActionResult> UpdatePlusStocktoStore(Guid IdSucursal,[FromForm] ExistenciaDto existenciadto)
        {
            if (existenciadto.Cantidad >= 0)
            {
                ModelState.AddModelError("Descripción",
                                        "Cantidad no valida");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var sucursal = await _RepositorioSucursal.ObtenerTienda(IdSucursal);
            var existencia = new ExistenciaCustomDto();

            if (sucursal == null )
            {
                ModelState.AddModelError("Descripción",
                                         "Sucursal no identificada");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            existencia = await _RepositorioExistencia.ObtenerExistenciaCustom(IdSucursal, existenciadto.IdProducto);

            if (existencia == null)
            {
                ModelState.AddModelError("Descripción",
                                         "Producto no identificado");
            }

            if ((existencia.Cantidad - existenciadto.Cantidad) < 0) {

                ModelState.AddModelError("Descripción",
                                            "Unidades insuficientes para la compra");
            }

            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            await _RepositorioExistencia.ActualizarExistencia(new Existencia() {
                                                                           IdSucursal = sucursal.Id,
                                                                           IdProducto = existencia.Id,
                                                                           Cantidad = existencia.Cantidad - existenciadto.Cantidad
                                                                         },

                                                                new Movimiento() {
                                                                                    IdProducto =existencia.Id,
                                                                                    IdSucursal= sucursal.Id,
                                                                                    IdTipoTrans = (int)TipoTransaccion.COMPRA,
                                                                                    Cantidad = existenciadto.Cantidad,
                                                                                    PrecioOperacion = existencia.Precio
                                                                                }
                                                                );

            return CreatedAtRoute("GetStorewithStock", new {IdSucursal = sucursal.Id}, null);
        }


        [HttpPut("tienda/{IdSucursal}/existencia/agregar")]
        public async Task<IActionResult> UpdateStocktoStore(Guid IdSucursal, [FromForm] ExistenciaDto existenciadto)
        {
            if (existenciadto.Cantidad == 0)
            {
                ModelState.AddModelError("Descripción",
                                        "Cantidad no valida");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var sucursal = await _RepositorioSucursal.ObtenerTienda(IdSucursal);
            var existencia = new ExistenciaCustomDto();

            if (sucursal == null)
            {
                ModelState.AddModelError("Descripción",
                                         "Sucursal no identificada");
            }

            else
            {

                existencia = await _RepositorioExistencia.ObtenerExistenciaCustom(IdSucursal, existenciadto.IdProducto);

                if (existencia == null)
                {
                    ModelState.AddModelError("Descripción",
                                             "Producto no identificado");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            await _RepositorioExistencia.ActualizarExistencia(new Existencia()
                                                        {
                                                            IdSucursal = sucursal.Id,
                                                            IdProducto = existencia.Id,
                                                            Cantidad = existencia.Cantidad + existenciadto.Cantidad
                                                        },

                                                        new Movimiento()
                                                        {
                                                            IdProducto = existencia.Id,
                                                            IdSucursal = sucursal.Id,
                                                            IdTipoTrans = (int)TipoTransaccion.AGREGAR,
                                                            Cantidad = existenciadto.Cantidad,
                                                            PrecioOperacion = existencia.Precio
                                                        }
                                                        );

            return CreatedAtRoute("GetStorewithStock", new { IdSucursal = sucursal.Id }, null);
        }


    }
}