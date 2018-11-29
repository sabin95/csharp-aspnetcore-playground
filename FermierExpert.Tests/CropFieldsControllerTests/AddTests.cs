using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FermierExpert.Tests.CropFieldsControllerTests
{
    public class AddTests
    {
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropFieldsController(MockDatabase.CreateNewDatabase());
            var response = controller.AddCropField(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropFieldsController(MockDatabase.CreateNewDatabase());
            var response = controller.AddCropField(new Commands.CropFieldCommand
            {
                Id = 0,
                ClientId = 1,
                CropId = 1
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Client_Id()
        {
            var controller = new CropFieldsController(MockDatabase.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 0, CropId = 1 };
            var response = controller.AddCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Crop_Id()
        {
            var controller = new CropFieldsController(MockDatabase.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 1, CropId = 0 };
            var response = controller.AddCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_CropField()
        {
            var controller = new CropFieldsController(MockDatabase.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 1, ClientId = 1, CropId = 1 };
            var response = controller.AddCropField(cropField);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new CropFieldsController(MockDatabase.CreateNewDatabase());
            var cropField = new CropFieldCommand { Id = 2, ClientId = 1, CropId = 1 };
            var response = controller.AddCropField(cropField);
            Assert.IsType<OkResult>(response);
        }
    }
}
