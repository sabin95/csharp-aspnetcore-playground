using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.StocksConstrollerTests
{
    public class AddTests
    {
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        } 

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var response = controller.Add(new Commands.StockCommand
            {
                Id = 0,
                ProductId = 1,
                ClientId = 1
            });
            var response2 = controller.Add(new Commands.StockCommand
            {
                Id = -4,
                ProductId = 1,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_ClientId()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 0 };
            var stock2 = new StockCommand { Id = 1, ProductId = 1, ClientId = -5 };
            var response = controller.Add(stock);
            var response2 = controller.Add(stock2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_ProductId()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 0, ClientId = 1 };
            var stock2 = new StockCommand { Id = 1, ProductId = -5, ClientId = 1 };
            var response = controller.Add(stock);
            var response2 = controller.Add(stock2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Product()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 1 };
            var response = controller.Add(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Client()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 7 };
            var response = controller.Add(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Product()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var stock = new StockCommand { Id = 1, ProductId = 7, ClientId = 1 };
            var response = controller.Add(stock);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new StocksController(MockDatabaseFactory.Create());
            var stock = new StockCommand { Id = 12, ProductId = 1, ClientId = 1 };
            var response = controller.Add(stock);
            Assert.IsType<OkResult>(response);
        }
    }
}
