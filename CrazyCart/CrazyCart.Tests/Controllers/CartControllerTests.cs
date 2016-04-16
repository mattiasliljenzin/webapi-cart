using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using CrazyCart.Controllers;
using CrazyCart.Factories;
using CrazyCart.Models;
using NUnit.Framework;
using Shouldly;

namespace CrazyCart.Tests.Controllers
{
    [TestFixture]
    public class CartControllerTests
    {
        private CartsController _controller;
        private Guid _existingCart;
        private Guid _existingProductId;

        [SetUp]
        public void Setup()
        {
            NinjectWebCommon.Start();
            
            var cartFactory = new InMemoryCartStorageFactory();
            var productFactory = new InMemoryProductStorageFactory();

            var cartStorage = cartFactory.Build();
            var productStorage = productFactory.Build();

            _controller = new CartsController(cartStorage, productStorage);

            _existingCart = cartStorage.GetAll().First().Id;
            _existingProductId = productStorage.GetAll().First().Id;
        }

        [Test]
        public void Can_post_cart()
        {
            // Arrange
            var cartModel = CreateCartModel();
            cartModel.Id = Guid.NewGuid();

            PrepareRequest();

            // Act
            var response = _controller.Post(cartModel);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            response.Headers.Location.ShouldBe(new Uri("http://localhost:8080/carts/" + cartModel.Id));
        }

        [Test]
        public void Can_get_cart()
        {
            // Arrange
            PrepareRequest();

            // Act
            var response = _controller.Get(_existingCart);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public void Can_put_cart()
        {
            // Arrange
            PrepareRequest();

            // Act
            var response = _controller.Put(_existingCart, new ProductRowUpdateModel
            {
                ProductId = _existingProductId,
                Quantity = 3
            });

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Test]
        public void Can_delete_cart()
        {
            // Arrange
            PrepareRequest();

            // Act
            var response = _controller.Delete(_existingCart);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        private void PrepareRequest(string url = null)
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/carts/" + url);
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }


        private CartModel CreateCartModel()
        {
            return new CartModel
            {
                Rows = new ProductRowModel[] { }
            };
        }
    }
}
