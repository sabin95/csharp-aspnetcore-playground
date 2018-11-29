using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.StocksConstrollerTests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new StocksController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new StocksController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(new Commands.StockCommand
            {
                Id = 0,
                ProductId = 1,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Stock()
        {
            var controller = new StocksController(MockDatabase.CreateNewDatabase());
            var stock = new StockCommand { Id = 7, ProductId = 1, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Product()
        {
            var controller = new StocksController(MockDatabase.CreateNewDatabase());
            var stock = new StockCommand { Id = 7, ProductId = 7, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new StocksController(MockDatabase.CreateNewDatabase());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 6 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new StocksController(MockDatabase.CreateNewDatabase());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<OkResult>(response);
        }
    }
}
