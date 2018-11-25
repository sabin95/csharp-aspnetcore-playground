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
    public class CompaniesController : ControllerBase
    {
        private readonly Database _database;

        public CompaniesController(Database database)
        {
            _database = database;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var companyResponses = new ListaDubluInlantuita<CompanyResponse>();
            foreach (var company in _database.Companies)
            {
                var products = new ListaDubluInlantuita<ProductResponse>();
                foreach (var product in _database.Products
                    .Where(x => x.CompanyId == company.Id)
                    .Select(x => new ProductResponse(x)))
                {
                    products.Add(product);

                }
                var response = new CompanyResponse(company)
                {
                    Products = products
                };
                companyResponses.Add(response);
            }
            return Ok(companyResponses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == id);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var products = new ListaDubluInlantuita<ProductResponse>();
            foreach (var product in _database.Products
                .Where(x => x.CompanyId == existingCompany.Id)
                .Select(x => new ProductResponse(x)))
            {
                products.Add(product);
            }
            var response = new CompanyResponse(existingCompany)
            {
                Products = products
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CompanyCommand companyCommand)
        {
            if (companyCommand is null)
            {
                return BadRequest();
            }
            if (companyCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == companyCommand.Id);
            if (existingCompany != null)
            {
                return BadRequest();
            }
            _database.Companies.Add(companyCommand);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CompanyCommand company)
        {
            if (company is null)
            {
                return BadRequest();
            }
            if (company.Id <= 0)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var indexOfExistingCompany = _database.Companies.IndexOf(existingCompany);
            _database.Companies[indexOfExistingCompany] = company;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == id);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            _database.Companies.Remove(existingCompany);
            return Ok();
        }

    }
}