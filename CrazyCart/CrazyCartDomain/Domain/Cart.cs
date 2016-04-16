using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyCartDomain.Domain
{
    public class Cart
    {
        public Cart()
        {
            Rows = new List<ProductRow>();
        }

        public Guid Id { get; set; }
        public IList<ProductRow> Rows { get; set; }

        public double TotalPriceIncVatAmount
        {
            get
            {
                return Rows.Sum(x => x.Product.PriceIncVat*x.Quantity);
            }
        }

        public double TotalVatAmount
        {
            get
            {
                return Rows.Sum(x => x.Product.VatAmount*x.Quantity);
            }
        }
    }
}