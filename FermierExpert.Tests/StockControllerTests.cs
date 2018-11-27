using FermierExpert.Commands;
using FermierExpert.Controllers;
using FermierExpert.Data;
using FermierExpert.Models;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FermierExpert.Tests
{
    public class StockControllerTests
    {
        #region Add
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var response = controller.Add(new Commands.StockCommand
            {
                Id = 0,
                ProductId = 1,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Product()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 1 };
            var response = controller.Add(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Client()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 7 };
            var response = controller.Add(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Product()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 1, ProductId = 7, ClientId = 1 };
            var response = controller.Add(stock);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 2, ProductId = 1, ClientId = 1 };
            var response = controller.Add(stock);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
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
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 7, ProductId = 1, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Product()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 7, ProductId = 7, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 6 };
            var response = controller.Update(stock);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new StocksController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } }, Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } } });
            var stock = new StockCommand { Id = 1, ProductId = 1, ClientId = 1 };
            var response = controller.Update(stock);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
