using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using CrazyCart.Models;
using CrazyCart.Setup;
using CrazyCartDomain.Domain;
using CrazyCartServiceContracts.Contracts;
using WebApiContrib.ModelBinders;

namespace CrazyCart.Controllers
{
    public class CartsController : ApiController
    {
        private readonly ICartStorage _cartStorage;
        private readonly IProductStorage _productStorage;

        public CartsController(ICartStorage cartStorage, IProductStorage productStorage)
        {
            _cartStorage = cartStorage;
            _productStorage = productStorage;
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cartStorage.GetAll().Select(Mapper.Map<CartModel>));
        }

        public HttpResponseMessage Get(Guid id)
        {
            var product = _cartStorage.Get(id);

            if (product == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, string.Format("Cannot find cart with id [{0}]", id));

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CartModel>(_cartStorage.Get(id)));
        }

        public HttpResponseMessage Post(CartModel cartModel)
        {
            _cartStorage.Upsert(Mapper.Map<Cart>(cartModel));

            var response = Request.CreateResponse(HttpStatusCode.Created, "Cart created successfully");
            response.Headers.Location = new Uri(string.Format("{0}/carts/{1}", ApplicationSettingsInstance.Instance.AppHostUrl, cartModel.Id));

            return response;
        }

        public HttpResponseMessage Put(Guid id, ProductRowUpdateModel productRowUpdate)
        {
            var cart = _cartStorage.Get(id);
            if (cart == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, string.Format("Cannot find cart for update with id [{0}]", id));

            var matchingProductInCart = cart.Rows.SingleOrDefault(x => x.Product.Id == productRowUpdate.ProductId);
            if (matchingProductInCart == null)
            {
                var product = _productStorage.Get(productRowUpdate.ProductId);
                if (product == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Cannot find the product you are trying to add");

                AddProductRowToCart(productRowUpdate, cart, product);
            }
            else
            {
                matchingProductInCart.Quantity = (int)productRowUpdate.Quantity;
            }

            _cartStorage.Upsert(cart);
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            response.Headers.Location = new Uri(string.Format("{0}/carts/{1}", ApplicationSettingsInstance.Instance.AppHostUrl, cart.Id));

            return response;
        }

        public HttpResponseMessage Delete(Guid id)
        {
            var cart = _cartStorage.Get(id);
            if (cart == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Could not find requested cartModel to remove");

            _cartStorage.Remove(cart.Id);

            return Request.CreateResponse(HttpStatusCode.OK, "Cart removed successfully");
        }

        private static void AddProductRowToCart(ProductRowUpdateModel productRowUpdate, Cart cart, Product product)
        {
            cart.Rows.Add(new ProductRow
            {
                Product = product,
                Quantity = (int) productRowUpdate.Quantity
            });
        }
    }
}