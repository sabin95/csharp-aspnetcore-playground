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
    public class ProductsController : ControllerBase
    {
        private readonly Database _database;
        private readonly IQueryHelper _queryExtensions;

        public ProductsController(Database database, IQueryHelper queryExtension)
        {
            _database = database;
            _queryExtensions = queryExtension;
        }
        [HttpGet("company/{companyId}")]
        public IActionResult GetProductByCompanyId(int companyId, GetAllBaseQuery<ProductCommand> query)
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
            var filteredList = _queryExtensions.WhereByColumns(_database.Products, query.FilterPayload);
            var orderedList = _queryExtensions
                .OrderByColumns(filteredList, query.SortColumns);
            var sortedList = _queryExtensions.Slice(orderedList, query.Start, query.Count);
            foreach (var product in sortedList
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
            _database.Products.Remove(existingProduct);
            return Ok();
        }
    }
}
