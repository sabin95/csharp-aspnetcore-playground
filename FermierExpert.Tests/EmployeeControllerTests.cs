using FermierExpert.Commands;
using FermierExpert.Controllers;
using FermierExpert.Data;
using FermierExpert.Models;
using FermierExpert.Services;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace FermierExpert.Tests
{
    public class EmployeesControllerTests
    {


        #region Add
        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var response = await controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var response = await controller.Add(new Commands.EmployeeCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Existing_Employee()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var empoyeeToAdd = new EmployeeCommand { Id = 1 };
            var response = await controller.Add(empoyeeToAdd);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Invalid_PhoneNumber()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 2, Phone = "073dsds0151058", Email = "dsadsadsa" };
            var response = await controller.Add(employee);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Invalid_Email()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 2, Phone = "073dsds0151058", Email = "dsadsadsa" };
            var response = await controller.Add(employee);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Ok()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 2, Phone = "+40730151058", Email = "dsads@adsa.ro" };
            var response = await controller.Add(employee);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var response = await controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var response = await controller.Update(new Commands.EmployeeCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_NonExisting_Employee()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var empoyeeToAdd = new EmployeeCommand { Id = 2 };
            var response = await controller.Update(empoyeeToAdd);
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_PhoneNumber()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 1, Phone = "073dsds0151058", Email = "dsadsadsa" };
            var response = await controller.Update(employee);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Bad_Request_On_Invalid_Email()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 1, Phone = "073dsds0151058", Email = "dsadsadsa" };
            var response = await controller.Update(employee);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Update_Should_Return_Ok()
        {
            var controller = new EmployeesController(new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } }, new RegexPhoneNumberValidator(), new RegexEmailAddressValidator());
            var employee = new EmployeeCommand { Id = 1, Phone = "+40730151058", Email = "dsads@adsa.ro" };
            var response = await controller.Update(employee);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
