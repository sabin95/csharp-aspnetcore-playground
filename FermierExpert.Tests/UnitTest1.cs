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
        private readonly EmployeesController _controller;
        private readonly Database testDatabase = new Database { Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } } };
        public EmployeesControllerTests()
        {
            _controller = new EmployeesController(testDatabase, new NumverifyPhoneNumberValidator());
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            // arrange

            // act
            var response = await _controller.Add(null);
            // assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var response = await _controller.Add(new Commands.EmployeeCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Existing_Employee()
        {

            var empoyeeToAdd = new EmployeeCommand { Id = 1 };
            var response = await _controller.Add(empoyeeToAdd);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Add_Should_Return_Bad_Request_On_Invalid_PhoneNumber()
        {
            var employee = new EmployeeCommand { Id = 2, Phone = "073dsds0151058" };
            var response = await _controller.Add(employee);
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
