using FermierExpert.Commands;
using FermierExpert.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class CropFieldController : Controller
    {
        [HttpGet("/client/{clientId}")]
        public IActionResult GetCropFieldsOfClient(int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == clientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            return Ok(Database.CropFields.Where(x => x.ClientId == existingClient.Id));
        }

        [HttpGet("{id}")]
        public IActionResult GetCropField(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            return Ok(Database.CropFields.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult AddCropField([FromBody] CropFieldCommand cropFieldCommand)
        {
            if (cropFieldCommand is null)
            {
                return BadRequest();
            }
            if (cropFieldCommand.Id <= 0)
            {
                return BadRequest();
            }
            if (cropFieldCommand.ClientId <= 0)
            {
                return BadRequest();
            }
            var alreadyExistingCropField = Database.CropFields.FirstOrDefault(x => x.Id == cropFieldCommand.Id);
            if (alreadyExistingCropField != null)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == cropFieldCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }

            Database.CropFields.Add(cropFieldCommand);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCropField([FromBody] CropFieldCommand cropFieldCommand)
        {
            if (cropFieldCommand is null)
            {
                return BadRequest();
            }
            if (cropFieldCommand.Id <= 0)
            {
                return BadRequest();
            }
            if (cropFieldCommand.ClientId <= 0)
            {
                return BadRequest();
            }
            var existingCropField = Database.CropFields.FirstOrDefault(x => x.Id == cropFieldCommand.Id);
            if (existingCropField is null)
            {
                return BadRequest();
            }
            var indexOfExistingCropField = Database.CropFields.IndexOf(existingCropField);
            Database.CropFields[indexOfExistingCropField] = cropFieldCommand;
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCropField(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingCropField = Database.CropFields.FirstOrDefault(x => x.Id == id);
            if (existingCropField is null)
            {
                return BadRequest();
            }
            Database.CropFields.Remove(existingCropField);
            return Ok();
        }
    }
}
