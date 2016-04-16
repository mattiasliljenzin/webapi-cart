using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using CrazyCart.Extensions;
using CrazyCart.Models;
using CrazyCart.Setup;
using CrazyCartDomain.Domain;
using CrazyCartServiceContracts.Contracts;

namespace CrazyCart.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductStorage _productStorage;

        public ProductsController(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _productStorage.GetAll().Select(Mapper.Map<ProductModel>));
        }

        public HttpResponseMessage Get(Guid id)
        {
            var product = _productStorage.Get(id);

            if (product == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, string.Format("Cannot find product with id [{0}]", id));

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ProductModel>(_productStorage.Get(id)));
        }

        public HttpResponseMessage Post(ProductModel productModel)
        {
            _productStorage.Upsert(Mapper.Map<Product>(productModel));

            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(string.Format("{0}/products/{1}", ApplicationSettingsInstance.Instance.AppHostUrl, productModel.Id));

            return response;
        }

        public HttpResponseMessage Put(Guid id, ProductUpdateModel productUpdate)
        {
            if (id == default(Guid))
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cannot update a product created with id 0");

            var product = _productStorage.Get(id);
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, string.Format("Cannot find product for update with id [{0}]", id));

            product.UpdateProduct(productUpdate);
            _productStorage.Upsert(product);

            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            response.Headers.Location = new Uri(string.Format("{0}/products/{1}", ApplicationSettingsInstance.Instance.AppHostUrl, product.Id));

            return response;
        }

        public HttpResponseMessage Delete(Guid id)
        {
            var product = _productStorage.Get(id);
            if (product == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Could not find requested product to remove");

            _productStorage.Remove(product.Id);

            return Request.CreateResponse(HttpStatusCode.OK, "Product removed successfully");
        }
    }
}