using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Queries;
using FermierExpert.Responses;
using FermierExpert.Services.Contracts;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class CropsController : Controller
    {
        private readonly Database _database;
        private readonly IQueryHelper _queryExtensions;

        public CropsController(Database database, IQueryHelper queryExtension)
        {
            _database = database;
            _queryExtensions = queryExtension;
        }
        [HttpGet]
        public IActionResult Get(GetAllBaseQuery<CropCommand> query)
        {
            var cropsResponse = new ListaDubluInlantuita<CropResponse>();
            var filteredList = _queryExtensions.WhereByColumns(_database.Crops, query.FilterPayload);
            var orderedList = _queryExtensions
                .OrderByColumns(filteredList, query.SortColumns);
            var sortedList = _queryExtensions.Slice(orderedList, query.Start, query.Count);
            foreach (var crop in sortedList)
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
            var existingCrop = _database.Crops.FirstOrDefault(x => x.Id == id);
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
            var alreadyExistingCrop = _database.Crops.FirstOrDefault(x => x.Id == cropCommand.Id);
            if (alreadyExistingCrop != null)
            {
                return BadRequest();
            }
            _database.Crops.Add(cropCommand);
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
            var existingCrop = _database.Crops.FirstOrDefault(x => x.Id == cropCommand.Id);
            if (existingCrop is null)
            {
                return BadRequest();
            }
            var indexOfExistingCrop = _database.Crops.IndexOf(existingCrop);
            _database.Crops[indexOfExistingCrop] = cropCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCrop(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var cropToRemove = _database.Crops.FirstOrDefault(x => x.Id == id);
            if (cropToRemove is null)
            {
                return BadRequest();
            }
            var existingCropField = _database.CropFields.FirstOrDefault(x => x.CropId == cropToRemove.Id);
            if (existingCropField != null)
            {
                return BadRequest();
            }
            _database.Crops.Remove(cropToRemove);
            return Ok();
        }
    }
}
