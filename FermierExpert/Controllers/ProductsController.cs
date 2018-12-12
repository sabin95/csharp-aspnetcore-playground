using System.Linq;
using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Database _database;

        public ProductsController(Database database)
        {
            _database = database;
        }
        [HttpGet("company/{companyId}")]
        public IActionResult GetProductByCompanyId(int companyId)
        {
            if (companyId <= 0)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == companyId);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var productsResponse = new ListaDubluInlantuita<ProductResponse>();
            foreach (var product in _database.Products
                .Where(x => x.CompanyId == existingCompany.Id)
                .Select(x => new ProductResponse(x)))
            {
                product.Company = new CompanyResponse(existingCompany);
                productsResponse.Add(product);
            }
            return Ok(productsResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == id);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == existingProduct.CompanyId);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var response = new ProductResponse(existingProduct)
            {
                Company = new CompanyResponse(existingCompany)
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductCommand productCommand)
        {
            if (productCommand is null)
            {
                return BadRequest();
            }
            if (productCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == productCommand.Id);
            if (existingProduct != null)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == productCommand.CompanyId);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            _database.Products.Add(productCommand);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductCommand productCommand)
        {
            if (productCommand is null)
            {
                return BadRequest();
            }
            if (productCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == productCommand.Id);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == productCommand.CompanyId);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var indexOfExistingProduct = _database.Products.IndexOf(existingProduct);
            _database.Products[indexOfExistingProduct] = productCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingProduct = _database.Products.FirstOrDefault(x => x.Id == id);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingStock = _database.Stocks.FirstOrDefault(x => x.ProductId == existingProduct.Id);
            if (existingStock !=null)
            {
                return BadRequest();
            }
            _database.Products.Remove(existingProduct);
            return Ok();
        }
    }
}