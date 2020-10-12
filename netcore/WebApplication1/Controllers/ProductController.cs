using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Negocio.Dto;
using WebApplication1.Repositorio;
using WebApplication1.ViewModels;
using System.Net;
using ConsumeWebApi.Proxy;
using ConsumeWebApi.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {

        private readonly IRepositorioProducto _RepositorioProducto;
        public ProductController(IRepositorioProducto repositorioProducto) {
            _RepositorioProducto = repositorioProducto;
        }

        public async Task<IActionResult> Index()
        {
            // var tiendas = await _RepositorioSucursal.ObtenerTiendasAsync();

            //StoreViewModel tiendas = new StoreViewModel() { StoreList = await _RepositorioSucursal.ObtenerTiendasAsync() };
            string view = "";
            object model;
            var proxyResp = await _RepositorioProducto.ObtenerProductosAsync();


            if (proxyResp.ResponseMessage.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                view = "../ErrorProxy/Index";
                model = proxyResp.ResponseMessage;

                //return View("Error", proxyResp.ResponseMessage);
            }
            else
            {
                view = "../Product/Index";
                model = new ProductViewModel() {  ProductList = proxyResp.GetData, HttpStatusCode = proxyResp.ResponseMessage.StatusCode };


                //view = "../Message/Index";
                //model = proxyResp.ResponseMessage;

                //return View("Message", proxyResp.ResponseMessage);
            }

            return View(view, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductViewModel producto)
        {
            string view = "";
            object model;
            ProxyResponse<ProductoDto> proxyResp=new ProxyResponse<ProductoDto>();

            try
            {

                proxyResp = await _RepositorioProducto.AgregarProducto(producto.Product);
                //await _RepositorioSucursal.AgregarTienda(sucursal.Store);

                if ( proxyResp.ResponseMessage.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    view = "../ErrorProxy/Index";
                    model = proxyResp.ResponseMessage;

                    //return View("Error", proxyResp.ResponseMessage);
                }
                else
                {
                    var proxyResp01 = await _RepositorioProducto.ObtenerProductosAsync();

                    view = "../Product/Index";
                    model = new ProductViewModel() { ProductList = proxyResp01.GetData, HttpStatusCode = proxyResp.ResponseMessage.StatusCode };


                    //view = "../Message/Index";
                    //model = proxyResp.ResponseMessage;

                    //return View("Message", proxyResp.ResponseMessage);
                }


               // urn View(view, model);

            }
            catch (Exception ex)
            {
                view = "../ErrorProxy/Error";
                model = ex;
            }
           
           
            return View(view, model);
          
        } 
    }
}