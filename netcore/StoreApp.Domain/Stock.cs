using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreApp.Domain
{
    public partial class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }

        public float Price { get; set; }
    }
}
