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
    public class ProductControllerTests
    {
        #region Add
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var response = controller.Add(new Commands.ProductCommand
            {
                Id = 0,
                CompanyId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Product()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var product = new ProductCommand { Id = 1, CompanyId = 1 };
            var response = controller.Add(product);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Company()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var product = new ProductCommand { Id = 1, CompanyId = 7 };
            var response = controller.Add(product);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var product = new ProductCommand { Id = 2, CompanyId = 1 };
            var response = controller.Add(product);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var response = controller.Update(new Commands.ProductCommand
            {
                Id = 0,
                CompanyId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Product()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var product = new ProductCommand { Id = 7, CompanyId = 1 };
            var response = controller.Update(product);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Company()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var product = new ProductCommand { Id = 1, CompanyId = 6 };
            var response = controller.Update(product);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new ProductsController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } }, Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } } });
            var product = new ProductCommand { Id = 1, CompanyId = 1 };
            var response = controller.Update(product);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
