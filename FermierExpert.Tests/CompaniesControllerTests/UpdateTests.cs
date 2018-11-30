using FermierExpert.Commands;
using FermierExpert.Controllers;
using FermierExpert.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace FermierExpert.Tests.CompaniesControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase(), new RapidApiCountryValidator());
            var response = await controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase(), new RapidApiCountryValidator());
            var response = await controller.Update(new Commands.CompanyCommand
            {
                Id = 0,
                Country = "romania"
            });
            var response2 = await controller.Update(new Commands.CompanyCommand
            {
                Id = -4,
                Country = "romania"
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_Country()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase(), new RapidApiCountryValidator());
            var response = await controller.Update(new Commands.CompanyCommand
            {
                Id = 0,
                Country = "dsfsdf"
            });
        }

        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_NonExisting_Company()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase(), new RapidApiCountryValidator());
            var company = new CompanyCommand { Id = 2, Country = "romania" };
            var response = await controller.Update(company);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Ok()
        {
            var controller = new CompaniesController(MockDatabase.CreateNewDatabase(), new RapidApiCountryValidator());
            var company = new CompanyCommand { Id = 1, Country = "romania" };
            var response = await controller.Update(company);
            Assert.IsType<OkResult>(response);
        }
    }
}
