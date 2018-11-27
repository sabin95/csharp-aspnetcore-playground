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
    public class CompanyControllerTests
    {
        #region Add
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var response = controller.Add(new Commands.CompanyCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Company()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var company = new CompanyCommand { Id = 1 };
            var response = controller.Add(company);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var company = new CompanyCommand { Id = 2 };
            var response = controller.Add(company);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var response = controller.Update(new Commands.CompanyCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Company()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var company = new CompanyCommand { Id = 2 };
            var response = controller.Update(company);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new CompaniesController(new Database { Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } } });
            var company = new CompanyCommand { Id = 1 };
            var response = controller.Update(company);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
