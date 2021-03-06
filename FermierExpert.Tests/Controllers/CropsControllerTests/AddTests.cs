﻿using FermierExpert.Commands;
using FermierExpert.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FermierExpert.Tests.CropsControllerTests
{
    public class AddTests
    {
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropsController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var response = controller.AddCrop(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropsController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var response = controller.AddCrop(new Commands.CropCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Crop()
        {
            var controller = new CropsController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var crop = new CropCommand { Id = 1 };
            var response = controller.AddCrop(crop);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new CropsController(MockDatabaseFactory.Create(), MockQueryHelperFactory.Create());
            var crop = new CropCommand { Id = 12 };
            var response = controller.AddCrop(crop);
            Assert.IsType<OkResult>(response);
        }
    }
}
