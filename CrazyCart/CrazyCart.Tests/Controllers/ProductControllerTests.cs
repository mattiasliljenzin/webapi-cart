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
    public class ProductControllerTests
    {
        private ProductsController _controller;
        private Guid _existingProduct;

        [SetUp]
        public void Setup()
        {
            NinjectWebCommon.Start();
            
            var factory = new InMemoryProductStorageFactory();
            var productStorage = factory.Build();
            _controller = new ProductsController(productStorage);

            _existingProduct = productStorage.GetAll().First().Id;
        }

        [Test]
        public void Can_post_product()
        {
            // Arrange
            var productModel = CreateProductModel();
            productModel.Id = Guid.NewGuid();

            PrepareRequest();

            // Act
            var response = _controller.Post(productModel);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            response.Headers.Location.ShouldBe(new Uri("http://localhost:8080/products/" + productModel.Id));
        }

        [Test]
        public void Can_get_product()
        {
            // Arrange
            PrepareRequest();

            // Act
            var response = _controller.Get(_existingProduct);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public void Can_put_product()
        {
            // Arrange
            PrepareRequest();

            // Act
            var response = _controller.Put(_existingProduct, new ProductUpdateModel
            {
                Name = "My shiny new iphone - updated!",
                PriceIncVat = 4999,
                VatPercentage = new decimal(0.20)
            });

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Test]
        public void Can_delete_product()
        {
            // Arrange
            PrepareRequest();

            // Act
            var response = _controller.Delete(_existingProduct);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        private void PrepareRequest(string url = null)
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/products/" + url);
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }


        private ProductModel CreateProductModel()
        {
            return new ProductModel
            {
                Name = "My shiny new iphone",
                PriceIncVat = 5000.0,
                VatPercentage = new decimal(0.25)
            };
        }
    }
}
