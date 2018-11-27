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
    public class CropControllerTests
    {
        #region Add
        [Fact]
        public void Add_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var response = controller.AddCrop(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var response = controller.AddCrop(new Commands.CropCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Add_Should_Return_Bad_Request_On_Existing_Crop()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var crop = new CropCommand { Id = 1 };
            var response = controller.AddCrop(crop);
            Assert.IsType<BadRequestResult>(response);
        }


        [Fact]
        public void Add_Should_Return_Ok()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var crop = new CropCommand { Id = 2 };
            var response = controller.AddCrop(crop);
            Assert.IsType<OkResult>(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_Should_Return_Bad_Request_On_Null_Commnad()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var response = controller.UpdateCrop(null);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Bad_Request_On_Invalid_Id()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var response = controller.UpdateCrop(new Commands.CropCommand
            {
                Id = 0
            });
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Update_Should_Return_Bad_Request_On_NonExisting_Crop()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var crop = new CropCommand { Id = 2 };
            var response = controller.UpdateCrop(crop);
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Update_Should_Return_Ok()
        {
            var controller = new CropsController(new Database { Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } } });
            var crop = new CropCommand { Id = 1 };
            var response = controller.UpdateCrop(crop);
            Assert.IsType<OkResult>(response);
        }
        #endregion
    }
}
