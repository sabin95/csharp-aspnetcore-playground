using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.ClientsControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Id_value0()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(new Commands.ClientCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_negative_id()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var response = controller.Update(new Commands.ClientCommand
            {
                Id = -5
            });
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var clientToAdd = new ClientCommand { Id = 2 };
            var response = controller.Update(clientToAdd);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new ClientsController(MockDatabase.CreateNewDatabase());
            var client = new ClientCommand { Id = 1 };
            var response = controller.Update(client);
            Assert.IsType<OkResult>(response);
        }
    }
}
