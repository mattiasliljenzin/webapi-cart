using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyCart.Models
{
    public class CartModel
    {
        public CartModel()
        {
            Rows = new List<ProductRowModel>();
        }

        public Guid Id { get; set; }
        public IList<ProductRowModel> Rows { get; set; }

        public double TotalPriceIncVatAmount { get { return Rows.Sum(x => x.PriceIncVat * x.Quantity); } }
        public double TotalVatAmount { get { return Rows.Sum(x => x.VatAmount * x.Quantity); } }
    }
}