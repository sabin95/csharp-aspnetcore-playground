using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class CropController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var cropsResponse = new ListaDubluInlantuita<CropResponse>();
            foreach (var crop in Database.Crops)
            {
                cropsResponse.Add(new CropResponse(crop));
            }

            return Ok(cropsResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetCrop(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingCrop = Database.Crops.FirstOrDefault(x => x.Id == id);
            if (existingCrop == null)
            {
                return BadRequest();
            }
            return Ok(new CropResponse(existingCrop));
        }

        [HttpPost]
        public IActionResult AddCrop([FromBody] CropCommand cropCommand)
        {
            if (cropCommand is null)
            {
                return BadRequest();
            }
            if (cropCommand.Id <= 0)
            {
                return BadRequest();
            }
            var alreadyExistingCrop = Database.Crops.FirstOrDefault(x => x.Id == cropCommand.Id);
            if (alreadyExistingCrop != null)
            {
                return BadRequest();
            }
            Database.Crops.Add(cropCommand);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCrop([FromBody] CropCommand cropCommand)
        {
            if (cropCommand is null)
            {
                return BadRequest();
            }
            if (cropCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingCrop = Database.Crops.FirstOrDefault(x => x.Id == cropCommand.Id);
            if (existingCrop is null)
            {
                return BadRequest();
            }
            var indexOfExistingCrop = Database.Crops.IndexOf(existingCrop);
            Database.Crops[indexOfExistingCrop] = cropCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCrop(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var cropToRemove = Database.Crops.FirstOrDefault(x => x.Id == id);
            if (cropToRemove is null)
            {
                return BadRequest();
            }
            Database.Crops.Remove(cropToRemove);
            return Ok();
        }
    }
}
