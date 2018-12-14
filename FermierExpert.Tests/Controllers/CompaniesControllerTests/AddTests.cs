using FermierExpert.Commands;
using FermierExpert.Controllers;
using FermierExpert.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace FermierExpert.Tests.CompaniesControllerTests
{
    public class AddTests
    {
        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CompaniesController(MockDatabaseFactory.Create(), new RapidApiCountryValidator(),MockQueryHelperFactory.Create());
            var response = await controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Invalid_id()
        {
            var controller = new CompaniesController(MockDatabaseFactory.Create(), new RapidApiCountryValidator(), MockQueryHelperFactory.Create());
            var response = await controller.Add(new Commands.CompanyCommand
            {
                Id = 0,
                Country = "romania"
            });
            var response2 = await controller.Add(new Commands.CompanyCommand
            {
                Id = -4,
                Country = "romania"
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Invalid_Country()
        {
            var controller = new CompaniesController(MockDatabaseFactory.Create(), new RapidApiCountryValidator(), MockQueryHelperFactory.Create());
            var response = await controller.Add(new Commands.CompanyCommand
            {
                Id = 0,
                Country = "dsfsdf"
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Existing_Company()
        {
            var controller = new CompaniesController(MockDatabaseFactory.Create(), new RapidApiCountryValidator(), MockQueryHelperFactory.Create());
            var company = new CompanyCommand { Id = 1, Country = "romania" };
            var response = await controller.Add(company);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public async Task Add_Should_Return_Ok()
        {
            var controller = new CompaniesController(MockDatabaseFactory.Create(), new RapidApiCountryValidator(), MockQueryHelperFactory.Create());
            var company = new CompanyCommand { Id = 12, Country = "romania" };
            var response = await controller.Add(company);
            Assert.IsType<OkResult>(response);
        }
    }
}
