using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("company/{companyId}")]
        public IActionResult GetProductByCompanyId(int companyId)
        {
            if (companyId <= 0)
            {
                return BadRequest();
            }
            var existingCompany = Database.Companies.FirstOrDefault(x => x.Id == companyId);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var productsResponse = new ListaDubluInlantuita<ProductResponse>();
            foreach (var product in Database.Products
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
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == id);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingCompany = Database.Companies.FirstOrDefault(x => x.Id == existingProduct.CompanyId);
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
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == productCommand.Id);
            if (existingProduct != null)
            {
                return BadRequest();
            }
            var existingCompany = Database.Companies.FirstOrDefault(x => x.Id == productCommand.CompanyId);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            Database.Products.Add(productCommand);
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
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == productCommand.Id);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            var existingCompany = Database.Companies.FirstOrDefault(x => x.Id == productCommand.CompanyId);
            if (existingCompany is null)
            {
                return BadRequest();
            }
            var indexOfExistingProduct = Database.Products.IndexOf(existingProduct);
            Database.Products[indexOfExistingProduct] = productCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingProduct = Database.Products.FirstOrDefault(x => x.Id == id);
            if (existingProduct is null)
            {
                return BadRequest();
            }
            Database.Products.Remove(existingProduct);
            return Ok();
        }
    }
}