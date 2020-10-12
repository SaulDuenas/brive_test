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

namespace WebApplication1.Controllers
{
    public class StoreController : Controller
    {

        private readonly IRepositorioSucursal _RepositorioSucursal;
        public StoreController(IRepositorioSucursal repositoriosucursal) {
            _RepositorioSucursal = repositoriosucursal;
        }

        public async Task<IActionResult> Index()
        {
            // var tiendas = await _RepositorioSucursal.ObtenerTiendasAsync();

            //StoreViewModel tiendas = new StoreViewModel() { StoreList = await _RepositorioSucursal.ObtenerTiendasAsync() };
            string view = "";
            object model;
            var proxyResp = await _RepositorioSucursal.ObtenerTiendasAsync();


            if (proxyResp.ResponseMessage.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                view = "../ErrorProxy/Index";
                model = proxyResp.ResponseMessage;

                //return View("Error", proxyResp.ResponseMessage);
            }
            else
            {
                view = "../Store/Index";
                model = new StoreViewModel() { StoreList = proxyResp.GetData, HttpStatusCode = proxyResp.ResponseMessage.StatusCode };


                //view = "../Message/Index";
                //model = proxyResp.ResponseMessage;

                //return View("Message", proxyResp.ResponseMessage);
            }

            return View(view, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStore(StoreViewModel sucursal)
        {
            string view = "";
            object model;
            ProxyResponse<SucursalDto> proxyResp=new ProxyResponse<SucursalDto>();

            try
            {

                proxyResp = await _RepositorioSucursal.AgregarTienda(sucursal.Store);
                //await _RepositorioSucursal.AgregarTienda(sucursal.Store);

                if ( proxyResp.ResponseMessage.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    view = "../ErrorProxy/Index";
                    model = proxyResp.ResponseMessage;

                    //return View("Error", proxyResp.ResponseMessage);
                }
                else
                {
                    var proxyResp01 = await _RepositorioSucursal.ObtenerTiendasAsync();

                    view = "../Store/Index";
                    model = new StoreViewModel() { StoreList = proxyResp01.GetData, HttpStatusCode = proxyResp.ResponseMessage.StatusCode };


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