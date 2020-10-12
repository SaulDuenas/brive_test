using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Negocio.Dto;
using StoreApp.Negocio.Model;
using StoreApp.Negocio.Repositorio;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/empresa")]
    public class ControladorProducto:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositorioProducto _RepositorioProducto;

        // Assign the object in the constructor for dependency injection
        public ControladorProducto(IMapper mapper, IRepositorioProducto RepositorioProducto)
        {
            _mapper = mapper;
            _RepositorioProducto = RepositorioProducto;
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProducts()
        {
            try {

                var productos = await _RepositorioProducto.ObtenerProductos();

                if (productos != null && productos.Count() > 0)
                {
                    var productresult = productos.Select(_mapper.Map<Producto, ProductoDto>);
                    return Ok(productresult);
                }
                else 
                {
                    return NoContent(); 
                }

            }
            catch (Exception ex) {

                // SDB: Manejo de log
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();

        }


        [HttpGet("producto/{IdProducto}", Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(Guid IdProducto)
        {
            try
            {
                var producto = await _RepositorioProducto.ObtenerProducto(IdProducto);

                if (producto != null )
                {
                    var productresult = _mapper.Map<Producto, ProductoDto>(producto);
                    return Ok(productresult);
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

        [HttpPost("producto/agregar")]
        public async Task<IActionResult> AgregarProducto([FromForm] ProductoDto productodto) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (await _RepositorioProducto.ExistenombreProducto(productodto.Nombre.Trim()))
            {
                ModelState.AddModelError("Descripción",
                                         "El nombre de Producto ya esta en uso");

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var newProduct = new Producto() {
                                                Nombre = productodto.Nombre.Trim(),
                                                CodigoBarras = productodto.CodigoBarras.Trim(),
                                                Precio = productodto.Precio
                                            };

            await _RepositorioProducto.AgregarProducto(newProduct);

            return CreatedAtRoute("GetProduct", new { IdProducto = newProduct.Id }, null);
               
        }

    }
}
