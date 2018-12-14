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
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var response = controller.Update(new Commands.StockCommand
            {
                Id = 0,
                ProductId = 1,
                ClientId = 1
            });
            var response2 = controller.Update(new Commands.StockCommand
            {
                Id = -4,
                ProductId = 1,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_ClientId()
        {
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 0 };
            var stock2 = new StockCommand { Id = 1, ProductId = 1, ClientId = -5 };
            var response = controller.Update(stock);
            var response2 = controller.Update(stock2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_ProductId()
        {
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 0, ClientId = 1 };
            var stock2 = new StockCommand { Id = 1, ProductId = -5, ClientId = 1 };
            var response = controller.Update(stock);
            var response2 = controller.Update(stock2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Stock()
        {
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var stock = new StockCommand { Id = 7, ProductId = 1, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Product()
        {
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var stock = new StockCommand { Id = 7, ProductId = 7, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 600 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new StocksController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<OkResult>(response);
        }
    }
}
