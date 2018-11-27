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
    public class VisitControllerTests
    {
        #region Add
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var response = controller.Add(new Commands.VisitCommand
            {
                Id = 0,
                EmployeeId = 1,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Visit()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = 1 };
            var response = controller.Add(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Client()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = 7 };
            var response = controller.Add(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Employee()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 1, EmployeeId = 7, ClientId = 1 };
            var response = controller.Add(visit);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 2, EmployeeId = 1, ClientId = 1 };
            var response = controller.Add(visit);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var response = controller.Update(new Commands.VisitCommand
            {
                Id = 0,
                EmployeeId = 1,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Visit()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 7, EmployeeId = 1, ClientId = 1 };
            var response = controller.Update(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Employee()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 1, EmployeeId = 7, ClientId = 1 };
            var response = controller.Update(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = 6 };
            var response = controller.Update(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new VisitsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } }, Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } } });
            var visit = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = 1 };
            var response = controller.Update(visit);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
