using FermierExpert.Commands;
using FermierExpert.Controllers;
using FermierExpert.Data;
using FermierExpert.Models;
using FermierExpert.Services;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FermierExpert.Tests
{
    public class ClientControllerTests
    {
        #region Add
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var response = controller.Add(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var response = controller.Add(new Commands.ClientCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Client()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var clientToAdd = new ClientCommand { Id = 1 };
            var response = controller.Add(clientToAdd);
            Assert.IsType<BadRequestResult>(response);
        }

        
        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var client = new ClientCommand { Id = 2};
            var response = controller.Add(client);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var response = controller.Update(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var response = controller.Update(new Commands.ClientCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var clientToAdd = new ClientCommand { Id = 2 };
            var response = controller.Update(clientToAdd);
            Assert.IsType<BadRequestResult>(response);
        }
     
        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new ClientsController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var client = new ClientCommand { Id = 1};
            var response = controller.Update(client);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
