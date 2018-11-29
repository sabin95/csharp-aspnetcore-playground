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
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Id_Value0()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var response = controller.Add(new Commands.ClientCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Negative_Id()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var response = controller.Add(new Commands.ClientCommand
            {
                Id = -4
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Client()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var clientToAdd = new ClientCommand { Id = 1 };
            var response = controller.Add(clientToAdd);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var client = new ClientCommand { Id = 2 };
            var response = controller.Add(client);
            Assert.IsType<OkResult>(response);
        }
    }
}
