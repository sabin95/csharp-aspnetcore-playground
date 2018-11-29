using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class CropFieldsController : Controller
    {
        private readonly Database _database;
            
        public CropFieldsController(Database database)
        {
            _database = database;
        }
        [HttpGet("client/{clientId}")]
        public IActionResult GetCropFieldsOfClient(int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == clientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var cropFieldResponse = new ListaDubluInlantuita<CropFieldResponse>();
            foreach (var cropField in _database.CropFields
                .Where(x => x.ClientId == existingClient.Id)
                .Select(x => new CropFieldResponse(x)))
            {
                cropField.Client = new ClientResponse(existingClient);
                var existingCrop = _database.Crops.FirstOrDefault(x => x.Id == cropField.CropId);
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
            var existingCropField = _database.CropFields.FirstOrDefault(x => x.Id == id);
            if (existingCropField == null)
            {
                return BadRequest();
            }
            var response = new CropFieldResponse(existingCropField);
            var existingCLient = _database.Clients.FirstOrDefault(x => x.Id == existingCropField.ClientId);
            if (existingCLient != null)
            {
                response.Client = new ClientResponse(existingCLient);
            }
            var existingCrop = _database.Crops.FirstOrDefault(x => x.Id == existingCropField.CropId);
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
            if (cropFieldCommand.CropId <= 0)
            {
                return BadRequest();
            }
            var alreadyExistingCropField = _database.CropFields.FirstOrDefault(x => x.Id == cropFieldCommand.Id);
            if (alreadyExistingCropField != null)
            {
                return BadRequest();
            }
            var alreadyExistingCrop = _database.Crops.FirstOrDefault(x => x.Id == cropFieldCommand.CropId);
            if (alreadyExistingCrop is null)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == cropFieldCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }

            _database.CropFields.Add(cropFieldCommand);
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
            if (cropFieldCommand.CropId <= 0)
            {
                return BadRequest();
            }
            var existingCropField = _database.CropFields.FirstOrDefault(x => x.Id == cropFieldCommand.Id);
            if (existingCropField is null)
            {
                return BadRequest();
            }
            var alreadyExistingCrop = _database.Crops.FirstOrDefault(x => x.Id == cropFieldCommand.CropId);
            if (alreadyExistingCrop is null)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == cropFieldCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var indexOfExistingCropField = _database.CropFields.IndexOf(existingCropField);
            _database.CropFields[indexOfExistingCropField] = cropFieldCommand;
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCropField(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingCropField = _database.CropFields.FirstOrDefault(x => x.Id == id);
            if (existingCropField is null)
            {
                return BadRequest();
            }
            _database.CropFields.Remove(existingCropField);
            return Ok();
        }
    }
}
