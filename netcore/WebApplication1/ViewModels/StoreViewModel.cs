using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class StoreViewModel
    {
       public IEnumerable<StoreApp.Negocio.Dto.SucursalDto> StoreList { get; set; }
       public StoreApp.Negocio.Dto.SucursalDto Store { get; set; }
       public System.Net.HttpStatusCode HttpStatusCode { get; set; }
       public string Message { get; set; }

       public bool tenemossucursales() {

            return StoreList != null && StoreList.Count() > 0 &&(

                    HttpStatusCode.Equals(System.Net.HttpStatusCode.OK) || 
                    HttpStatusCode.Equals(System.Net.HttpStatusCode.BadRequest) || 
                    HttpStatusCode.Equals(System.Net.HttpStatusCode.Created)
                );
        }

    }
}
