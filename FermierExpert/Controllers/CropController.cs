using FermierExpert.Data;
using FermierExpert.Models;
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
            return Ok(Database.Crops);
        }

        [HttpGet("{id}")]
        public IActionResult GetCrop(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            return Ok(Database.Crops.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult AddCrop([FromBody] Crop crop)
        {
            if (crop is null)
            {
                return BadRequest();
            }
            if (crop.Id <= 0)
            {
                return BadRequest();
            }
            var alreadyExistingCrop = Database.Crops.FirstOrDefault(x => x.Id == crop.Id);
            if (alreadyExistingCrop != null)
            {
                return BadRequest();
            }
            Database.Crops.Add(crop);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCrop([FromBody] Crop crop)
        {
            if (crop is null)
            {
                return BadRequest();
            }
            if (crop.Id <= 0)
            {
                return BadRequest();
            }
            var existingCrop = Database.Crops.FirstOrDefault(x => x.Id == crop.Id);
            if (existingCrop is null)
            {
                return BadRequest();
            }
            var indexOfExistingCrop = Database.Crops.IndexOf(existingCrop);
            Database.Crops[indexOfExistingCrop] = crop;
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
