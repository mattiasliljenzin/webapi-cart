using CrazyCart.Models;
using CrazyCartDomain.Domain;

namespace CrazyCart.Extensions
{
    public static class ProductExtensionMethods
    {
        public static Product UpdateProduct(this Product product, ProductUpdateModel update)
        {
            product.Name = update.Name;
            product.PriceIncVat = update.PriceIncVat;
            product.VatPercentage = update.VatPercentage;

            return product;
        }
    }
}