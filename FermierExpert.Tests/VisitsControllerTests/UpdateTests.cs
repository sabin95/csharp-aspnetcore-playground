using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.VisitsControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.Update(new Commands.VisitCommand
            {
                Id = 0,
                EmployeeId = 1,
                ClientId = 1
            });
            var response2 = controller.Update(new Commands.VisitCommand
            {
                Id = -4,
                EmployeeId = 1,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_EmployeeId()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var visit = new VisitCommand { Id = 1, EmployeeId = 0, ClientId = 1 };
            var visit2 = new VisitCommand { Id = 1, EmployeeId = -5, ClientId = 1 };
            var response = controller.Update(visit);
            var response2 = controller.Update(visit2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_ClientId()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var visit = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = 0 };
            var visit2 = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = -5 };
            var response = controller.Update(visit);
            var response2 = controller.Update(visit2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Visit()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var visit = new VisitCommand { Id = 7, EmployeeId = 1, ClientId = 1 };
            var response = controller.Update(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Employee()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var visit = new VisitCommand { Id = 1, EmployeeId = 7, ClientId = 1 };
            var response = controller.Update(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var visit = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = 600 };
            var response = controller.Update(visit);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new VisitsController(MockDatabaseFactory.CreateNewDatabase());
            var visit = new VisitCommand { Id = 1, EmployeeId = 1, ClientId = 1 };
            var response = controller.Update(visit);
            Assert.IsType<OkResult>(response);
        }
    }
}
