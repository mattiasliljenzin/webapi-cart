using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Hosting;
using CrazyCart;
using CrazyCart.Controllers;
using CrazyCart.Factories;
using CrazyCart.Models;
using CrazyCart.Setup;
using NUnit.Framework;
using Shouldly;

namespace CrazyCartIntegrationTests
{
    [TestFixture]
    public class ProductTests
    {
        private ProductsController _controller;
        private readonly Guid _idForPost = Guid.NewGuid();

        [TestFixtureSetUp]
        public void Setup()
        {
            ComponentRegistry.RegisterAutoMapper();
            var factory = new InMemoryProductStorageFactory();
            _controller = new ProductsController(factory.Build());
        }

        [Test]
        public void T1_Can_post_product()
        {
            // Arrange
            var productModel = CreateProductModel();

            PrepareRequest();

            // Act
            var response = _controller.Post(productModel);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            response.Headers.Location.ShouldBe(new Uri("http://localhost:8080/products/" + _idForPost));
        }

        [Test]
        public void T2_Can_get_product()
        {
            // Arrange
            PrepareRequest();

            // Act
            var response = _controller.Get(_idForPost);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Found);
        }

        private void PrepareRequest(string url = null)
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/products/" + url);
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        private ProductModel CreateProductModel()
        {
            var productModel = new ProductModel
            {
                Id = _idForPost,
                Name = "My shiny new iphone",
                PriceIncVat = 5000.0,
                VatPercentage = new decimal(0.25)
            };
            return productModel;
        }
    }
}
