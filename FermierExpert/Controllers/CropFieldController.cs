using FermierExpert.Data;
using FermierExpert.Models;
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
            return Ok(Database.CropFields.Where(x => x.Client.Id == existingClient.Id));
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
        public IActionResult AddCropField([FromBody] CropField cropField)
        {
            if (cropField is null)
            {
                return BadRequest();
            }
            if (cropField.Id <= 0)
            {
                return BadRequest();
            }
            if (cropField.Client is null)
            {
                return BadRequest();
            }
            var alreadyExistingCropField = Database.CropFields.FirstOrDefault(x => x.Id == cropField.Id);
            if (alreadyExistingCropField != null)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == cropField.Client.Id);
            if (existingClient is null)
            {
                return BadRequest();
            }

            Database.CropFields.Add(cropField);
            existingClient.Fields.Add(cropField);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCropField([FromBody] CropField cropField)
        {
            if (cropField is null)
            {
                return BadRequest();
            }
            if (cropField.Id <= 0)
            {
                return BadRequest();
            }
            if (cropField.Client is null)
            {
                return BadRequest();
            }
            var existingCropField = Database.CropFields.FirstOrDefault(x => x.Id == cropField.Id);
            if (existingCropField is null)
            {
                return BadRequest();
            }
            if (existingCropField.Client.Id != cropField.Client.Id)
            {
                var oldClient = Database.Clients.FirstOrDefault(x => x.Id == existingCropField.Id);
                oldClient.Fields.Remove(oldClient.Fields.FirstOrDefault(cf => cf.Id == cropField.Id));
                var newClient = Database.Clients.FirstOrDefault(x => x.Id == cropField.Client.Id);
                newClient.Fields.Add(cropField);
            }
            var indexOfExistingCropField = Database.CropFields.IndexOf(existingCropField);
            Database.CropFields[indexOfExistingCropField] = cropField;
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
