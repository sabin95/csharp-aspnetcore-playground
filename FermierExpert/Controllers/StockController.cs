using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Models;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet("clients/{clientId}")]
        public IActionResult GetStockByClient(int clientId)
        {
            if (clientId<=0)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x=>x.Id==clientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var stocksResponse = new ListaDubluInlantuita<StockResponse>();
            foreach (var stock in Database.Stocks
                .Where(x=>x.ClientId==existingClient.Id)
                .Select(x=> new StockResponse(x)))
            {
                stock.Client = new ClientResponse(existingClient);
                var existingProduct = Database.Products.FirstOrDefault(x => x.Id == stock.ProductId);
                if (existingProduct != null)
                {
                    stock.Product = new ProductResponse(existingProduct);
                }
                stocksResponse.Add(stock);
            }
            return Ok(stocksResponse);
        }

        [HttpGet("product/productId")]
        public IActionResult GetStocksByProductId (int productId)
        {
            if (productId<=0)
            {
                return BadRequest();
            }
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == productId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var stocksResponse = new ListaDubluInlantuita<StockResponse>();
            foreach (var stock in Database.Stocks
                .Where(x=>x.ProductId==existingProduct.Id)
                .Select(x=> new StockResponse(x)))
            {
                stock.Product = new ProductResponse(existingProduct);
                var existingClient = Database.Clients.FirstOrDefault(x=>x.Id == stock.ClientId);
                if (existingClient!=null)
                {
                    stock.Client = new ClientResponse(existingClient);
                }
                stocksResponse.Add(stock);
            }
            return Ok(stocksResponse);
        }

        [HttpGet]
        public IActionResult GetByStockId(int stockId)
        {
            if (stockId <=0)
            {
                return BadRequest();
            }
            var existingStock = Database.Stocks.FirstOrDefault(x => x.Id == stockId);
            if (existingStock is null)
            {
                return BadRequest();
            }
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == existingStock.ProductId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x=>x.Id==existingStock.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var response = new StockResponse(existingStock)
            {
                Product = new ProductResponse(existingProduct),
                Client = new ClientResponse(existingClient)
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] StockCommand stockCommand)
        {
            if(stockCommand is null)
            {
                return BadRequest();
            }
            if (stockCommand.Id<=0)
            {
                return BadRequest();
            }
            var existingStock = Database.Stocks.FirstOrDefault(x => x.Id == stockCommand.Id);
            if (existingStock != null)
            {
                return BadRequest();
            }
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == stockCommand.ProductId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x=>x.Id==stockCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            Database.Stocks.Add(stockCommand);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] StockCommand stockCommand)
        {
            if (stockCommand is null)
            {
                return BadRequest();
            }
            if (stockCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingStock = Database.Stocks.FirstOrDefault(x => x.Id == stockCommand.Id);
            if (existingStock is null)
            {
                return BadRequest();
            }
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == stockCommand.ProductId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == stockCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var indexOfExistingStock = Database.Stocks.IndexOf(existingStock);
            Database.Stocks[indexOfExistingStock] = stockCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            var existingStock = Database.Stocks.FirstOrDefault(x=>x.Id==id);
            if (existingStock is null)
            {
                return BadRequest();
            }
            Database.Stocks.Remove(existingStock);
            return Ok();
        }

    }
}