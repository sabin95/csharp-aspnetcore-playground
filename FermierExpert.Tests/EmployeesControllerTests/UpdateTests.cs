using FermierExpert.Commands;
using FermierExpert.Controllers;
using FermierExpert.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace FermierExpert.Tests.EmployeesControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new EmployeesController(MockDatabase.CreateNewDatabase(), new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var response = await controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new EmployeesController(MockDatabase.CreateNewDatabase(), new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var response = await controller.Update(new Commands.EmployeeCommand
            {
                Id = 0
            });
            var response2 = await controller.Update(new Commands.EmployeeCommand
            {
                Id = -5
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }
        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_NonExisting_Employee()
        {
            var controller = new EmployeesController(MockDatabase.CreateNewDatabase(), new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var empoyeeToAdd = new EmployeeCommand { Id = 2 };
            var response = await controller.Update(empoyeeToAdd);
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_PhoneNumber()
        {
            var controller = new EmployeesController(MockDatabase.CreateNewDatabase(), new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 1, Phone = "073dsds0151058", Email = "dsadsadsa" };
            var response = await controller.Update(employee);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_Email()
        {
            var controller = new EmployeesController(MockDatabase.CreateNewDatabase(), new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 1, Phone = "073dsds0151058", Email = "dsadsadsa" };
            var response = await controller.Update(employee);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Ok()
        {
            var controller = new EmployeesController(MockDatabase.CreateNewDatabase(), new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 1, Phone = "+40730151058", Email = "dsads@adsa.ro" };
            var response = await controller.Update(employee);
            Assert.IsType<OkResult>(response);
        }
    }
}
