using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Responses;
using ListaDubluInlantuita;
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
            var cropFieldResponse = new ListaDubluInlantuita<CropFieldResponse>();
            foreach (var cropField in Database.CropFields
                .Where(x => x.ClientId == existingClient.Id)
                .Select(x => new CropFieldResponse(x)))
            {
                cropField.Client = new ClientResponse(existingClient);
                var existingCrop = Database.Crops.FirstOrDefault(x => x.Id == cropField.CropId);
                if (existingCrop != null)
                {
                    cropField.Crop = new CropResponse(existingCrop);
                }
                cropFieldResponse.Add(cropField);
            }
            return Ok(cropFieldResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetCropField(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var existingCropField = Database.CropFields.FirstOrDefault(x => x.Id == id);
            if (existingCropField == null)
            {
                return BadRequest();
            }
            var response = new CropFieldResponse(existingCropField);
            var existingCLient = Database.Clients.FirstOrDefault(x => x.Id == existingCropField.ClientId);
            if (existingCLient != null)
            {
                response.Client = new ClientResponse(existingCLient);
            }
            var existingCrop = Database.Crops.FirstOrDefault(x => x.Id == existingCropField.CropId);
            if (existingCrop != null)
            {
                response.Crop = new CropResponse(existingCrop);
            }
            return Ok(response);
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
