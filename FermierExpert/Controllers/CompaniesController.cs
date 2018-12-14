using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CompaniesController : ControllerBase
    {
        private readonly Database _database;
        private readonly ICountryValidator _countryValidator;
        private readonly IQueryHelper _queryExtensions;

        public CompaniesController(Database database, ICountryValidator countryValidator, IQueryHelper queryExtensions)
        {
            _database = database;
            _countryValidator = countryValidator;
            _queryExtensions = queryExtensions;
        }

        [HttpGet]
        public IActionResult Get(GetAllBaseQuery<CompanyCommand> query)
        {
            
            var companyResponses = new ListaDubluInlantuita<CompanyResponse>();
            var filteredList = _queryExtensions.WhereByColumns(_database.Companies, query.FilterPayload);
            var orderedList = _queryExtensions
                .OrderByColumns(filteredList, query.SortColumns);
            var sortedList = _queryExtensions.Slice(orderedList, query.Start, query.Count);
            foreach (var company in sortedList)
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
        public async Task<IActionResult> Add([FromBody] CompanyCommand companyCommand)
        {
            if (companyCommand is null)
            {
                return BadRequest();
            }
            if (companyCommand.Id <= 0)
            {
                return BadRequest();
            }
            var countryList = await _countryValidator.SearchCountry(companyCommand.Country);
            if (countryList.Length != 1)
            {
                return BadRequest();
            }
            companyCommand.Country = countryList.FirstOrDefault().Name;
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == companyCommand.Id);
            if (existingCompany != null)
            {
                return BadRequest();
            }
            _database.Companies.Add(companyCommand);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CompanyCommand companyCommand)
        {
            if (companyCommand is null)
            {
                return BadRequest();
            }
            if (companyCommand.Id <= 0)
            {
                return BadRequest();
            }
            var countryList = await _countryValidator.SearchCountry(companyCommand.Country);
            if (countryList.Length != 1)
            {
                return BadRequest();
            }
            companyCommand.Country = countryList.FirstOrDefault().Name;
            var existingCompany = _database.Companies.FirstOrDefault(x => x.Id == companyCommand.Id);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var indexOfExistingCompany = _database.Companies.IndexOf(existingCompany);
            _database.Companies[indexOfExistingCompany] = companyCommand;
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
            var existingProduct = _database.Products.FirstOrDefault(x => x.CompanyId == existingCompany.Id);
            if (existingProduct !=null)
            {
                return BadRequest();
            }
            _database.Companies.Remove(existingCompany);
            return Ok();
        }

    }
}
