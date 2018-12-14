using System.Linq;
using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Queries;
using FermierExpert.Responses;
using FermierExpert.Services.Contracts;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;

namespace FermierExpert.Controllers
{

    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly Database _database;
        private readonly IQueryHelper _queryExtensions;
        public StocksController(Database database, IQueryHelper queryExtension)
        {
            _database = database;
            _queryExtensions = queryExtension;
        }
        [HttpGet("clients/{clientId}")]
        public IActionResult GetStockByClient(int clientId, GetAllBaseQuery<StockCommand> query)
        {
            if (clientId <= 0)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == clientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var stocksResponse = new ListaDubluInlantuita<StockResponse>();
            var filteredList = _queryExtensions.WhereByColumns(_database.Stocks, query.FilterPayload);
            var orderedList = _queryExtensions
                .OrderByColumns(filteredList, query.SortColumns);
            var sortedList = _queryExtensions.Slice(orderedList, query.Start, query.Count);
            foreach (var stock in sortedList
                .Where(x => x.ClientId == existingClient.Id)
                .Select(x => new StockResponse(x)))
            {
                stock.Client = new ClientResponse(existingClient);
                var existingProduct = _database.Products.FirstOrDefault(x => x.Id == stock.ProductId);
                if (existingProduct != null)
                {
                    stock.Product = new ProductResponse(existingProduct);
                }
                stocksResponse.Add(stock);
            }
            return Ok(stocksResponse);
        }

        [HttpGet("product/productId")]
        public IActionResult GetStocksByProductId(int productId, GetAllBaseQuery<StockCommand> query)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == productId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var stocksResponse = new ListaDubluInlantuita<StockResponse>();
            var filteredList = _queryExtensions.WhereByColumns(_database.Stocks, query.FilterPayload);
            var orderedList = _queryExtensions
                .OrderByColumns(filteredList, query.SortColumns);
            var sortedList = _queryExtensions.Slice(orderedList, query.Start, query.Count);
            foreach (var stock in sortedList
                .Where(x => x.ProductId == existingProduct.Id)
                .Select(x => new StockResponse(x)))
            {
                stock.Product = new ProductResponse(existingProduct);
                var existingClient = _database.Clients.FirstOrDefault(x => x.Id == stock.ClientId);
                if (existingClient != null)
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
            if (stockId <= 0)
            {
                return BadRequest();
            }
            var existingStock = _database.Stocks.FirstOrDefault(x => x.Id == stockId);
            if (existingStock is null)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == existingStock.ProductId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == existingStock.ClientId);
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
            if (stockCommand is null)
            {
                return BadRequest();
            }
            if (stockCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingStock = _database.Stocks.FirstOrDefault(x => x.Id == stockCommand.Id);
            if (existingStock != null)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == stockCommand.ProductId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == stockCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            _database.Stocks.Add(stockCommand);
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
            var existingStock = _database.Stocks.FirstOrDefault(x => x.Id == stockCommand.Id);
            if (existingStock is null)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == stockCommand.ProductId);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == stockCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var indexOfExistingStock = _database.Stocks.IndexOf(existingStock);
            _database.Stocks[indexOfExistingStock] = stockCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingStock = _database.Stocks.FirstOrDefault(x => x.Id == id);
            if (existingStock is null)
            {
                return BadRequest();
            }
            _database.Stocks.Remove(existingStock);
            return Ok();
        }

    }
}
