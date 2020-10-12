using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Domain
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string BarCode { get; set; }

        public decimal  Price { get; set; }

    }
}
