using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.ClientsControllerTests
{
    public class AddTests
    {
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ClientsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new ClientsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.Add(new Commands.ClientCommand
            {
                Id = 0
            });
            var response2 = controller.Add(new Commands.ClientCommand
            {
                Id = -4
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }
             

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Client()
        {
            var controller = new ClientsController(MockDatabaseFactory.CreateNewDatabase());
            var clientToAdd = new ClientCommand { Id = 1 };
            var response = controller.Add(clientToAdd);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new ClientsController(MockDatabaseFactory.CreateNewDatabase());
            var client = new ClientCommand { Id = 200 };
            var response = controller.Add(client);
            Assert.IsType<OkResult>(response);
        }
    }
}
