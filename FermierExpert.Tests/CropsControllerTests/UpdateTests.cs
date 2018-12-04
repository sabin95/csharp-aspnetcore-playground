using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FermierExpert.Tests.CropsControllerTests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.UpdateCrop(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropsController(MockDatabaseFactory.CreateNewDatabase());
            var response = controller.UpdateCrop(new Commands.CropCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Crop()
        {
            var controller = new CropsController(MockDatabaseFactory.CreateNewDatabase());
            var crop = new CropCommand { Id = 2 };
            var response = controller.UpdateCrop(crop);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new CropsController(MockDatabaseFactory.CreateNewDatabase());
            var crop = new CropCommand { Id = 1 };
            var response = controller.UpdateCrop(crop);
            Assert.IsType<OkResult>(response);
        }
    }
}
