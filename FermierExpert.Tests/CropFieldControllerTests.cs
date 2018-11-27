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
    public class CropFieldControllerTests
    {
        #region Add
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var response = controller.AddCropField(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var response = controller.AddCropField(new Commands.CropFieldCommand
            {
                Id = 0,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Client_Id()
        {
            var controller = new CropFieldController(new Database { CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } }, Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } } });
            var cropField = new CropFieldCommand { Id = 1, ClientId = 0 };
            var response = controller.AddCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_CropField()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var cropField = new CropFieldCommand { Id = 1, ClientId = 1 };
            var response = controller.AddCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var cropField = new CropFieldCommand { Id = 2, ClientId = 1 };
            var response = controller.AddCropField(cropField);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var response = controller.UpdateCropField(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var response = controller.UpdateCropField(new Commands.CropFieldCommand
            {
                Id = 0,
                ClientId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Client()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var cropField = new CropFieldCommand { Id = 2, ClientId = 1 };
            var response = controller.UpdateCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Client_id()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var cropField = new CropFieldCommand { Id = 1, ClientId = 0 };
            var response = controller.UpdateCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new CropFieldController(new Database { Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } }, CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } } });
            var cropField = new CropFieldCommand { Id = 1, ClientId = 1 };
            var response = controller.UpdateCropField(cropField);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
