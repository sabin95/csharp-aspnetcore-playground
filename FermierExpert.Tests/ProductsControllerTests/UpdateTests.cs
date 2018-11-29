using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.ProductsControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ProductsController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new ProductsController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(new Commands.ProductCommand
            {
                Id = 0,
                CompanyId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Product()
        {
            var controller = new ProductsController(MockDatabase.CreateNewDatabase());
            var product = new ProductCommand { Id = 7, CompanyId = 1 };
            var response = controller.Update(product);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Company()
        {
            var controller = new ProductsController(MockDatabase.CreateNewDatabase());
            var product = new ProductCommand { Id = 1, CompanyId = 6 };
            var response = controller.Update(product);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new ProductsController(MockDatabase.CreateNewDatabase());
            var product = new ProductCommand { Id = 1, CompanyId = 1 };
            var response = controller.Update(product);
            Assert.IsType<OkResult>(response);
        }
    }
}
