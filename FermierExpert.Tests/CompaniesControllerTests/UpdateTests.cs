using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.CompaniesControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(new Commands.CompanyCommand
            {
                Id = 0
            });
            var response2 = controller.Update(new Commands.CompanyCommand
            {
                Id = -4
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

      
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Company()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase());
            var company = new CompanyCommand { Id = 2 };
            var response = controller.Update(company);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase());
            var company = new CompanyCommand { Id = 1 };
            var response = controller.Update(company);
            Assert.IsType<OkResult>(response);
        }
    }
}
