using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeWebApi.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<StoreApp.Negocio.Dto.ProductoDto> ProductList { get; set; }
        public StoreApp.Negocio.Dto.ProductoDto Product { get; set; }
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }

        public bool tenemosProductos()
        {

            return ProductList != null && ProductList.Count() > 0 && (

                        HttpStatusCode.Equals(System.Net.HttpStatusCode.OK) ||
                        HttpStatusCode.Equals(System.Net.HttpStatusCode.BadRequest) ||
                        HttpStatusCode.Equals(System.Net.HttpStatusCode.Created)
                    );
        }

    }
}
