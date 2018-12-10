using FermierExpert.Commands;
using FermierExpert.Controllers;
using FermierExpert.Data;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.ProductsControllerTests
{
    public class AddTests
    {
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ProductsController(MockDatabaseFactory.Create());
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new ProductsController(MockDatabaseFactory.Create());
            var response = controller.Add(new Commands.ProductCommand
            {
                Id = 0,
                CompanyId = 1
            });
            var response2 = controller.Add(new Commands.ProductCommand
            {
                Id = -5,
                CompanyId = 1
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Company_Id()
        {
            var controller = new ProductsController(MockDatabaseFactory.Create());
            var product = new ProductCommand { Id = 1, CompanyId = 0 };
            var product2 = new ProductCommand { Id = 1, CompanyId = -4 };
            var response = controller.Add(product);
            var response2 = controller.Add(product2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Product()
        {
            var controller = new ProductsController(MockDatabaseFactory.Create());
            var product = new ProductCommand { Id = 1, CompanyId = 1 };
            var response = controller.Add(product);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Company()
        {
            var controller = new ProductsController(MockDatabaseFactory.Create());
            var product = new ProductCommand { Id = 1, CompanyId = 7 };
            var response = controller.Add(product);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new ProductsController(MockDatabaseFactory.Create());
            var product = new ProductCommand { Id = 12, CompanyId = 1 };
            var response = controller.Add(product);
            Assert.IsType<OkResult>(response);
        }
    }
}
