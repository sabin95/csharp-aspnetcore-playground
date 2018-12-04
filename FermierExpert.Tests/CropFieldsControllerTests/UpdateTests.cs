using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FermierExpert.Tests.CropFieldsControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.UpdateCropField(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.UpdateCropField(new Commands.CropFieldCommand
            {
                Id = 0,
                ClientId = 1,
                CropId = 1
            });
            var response2 = controller.UpdateCropField(new Commands.CropFieldCommand
            {
                Id = -5,
                ClientId = 1,
                CropId = 1
            });
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_CropField()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 2, ClientId = 1, CropId = 1 };
            var response = controller.UpdateCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Crop()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 1, CropId = 8 };
            var response = controller.UpdateCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Non_Existing_Client()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 8, CropId = 1 };
            var response = controller.UpdateCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Crop_Id()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 0, CropId = 1 };
            var cropField2 = new CropFieldCommand { Id = 1, ClientId = -4, CropId = 1 };
            var response = controller.UpdateCropField(cropField);
            var response2 = controller.UpdateCropField(cropField2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Client_id()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 1, CropId = 0 };
            var cropField2 = new CropFieldCommand { Id = 1, ClientId = 1, CropId = -5 };
            var response = controller.UpdateCropField(cropField);
            var response2 = controller.UpdateCropField(cropField2);
            Assert.IsType<BadRequestResult>(response);
            Assert.IsType<BadRequestResult>(response2);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new CropFieldsController(MockDatabaseFactory.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 1, CropId = 1 };
            var response = controller.UpdateCropField(cropField);
            Assert.IsType<OkResult>(response);
        }
    }
}
