using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var clientsResponse = new ListaDubluInlantuita<ClientResponse>();
            foreach (var client in Database.Clients)
            {
                var cropFields = new ListaDubluInlantuita<CropFieldResponse>();
                foreach (var cropField in Database.CropFields
                .Where(x => x.ClientId == client.Id)
                .Select(x => new CropFieldResponse(x)))
                {
                    cropFields.Add(cropField);
                }
                var response = new ClientResponse(client)
                {
                    Fields = cropFields
                };
                clientsResponse.Add(response);
            }
            return Ok(clientsResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == id);
            if (existingClient is null)
            {
                return BadRequest();
            }

            var cropFields = new ListaDubluInlantuita<CropFieldResponse>();
            foreach (var cropField in Database.CropFields
                .Where(x => x.ClientId == id)
                .Select(x => new CropFieldResponse(x)))
            {
                cropFields.Add(cropField);
            }

            var response = new ClientResponse(existingClient)
            {
                Fields = cropFields
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ClientCommand clientCommand)
        {
            if (clientCommand is null)
            {
                return BadRequest();
            }
            if (clientCommand.Id <= 0)
            {
                return BadRequest();
            }
            var alreadyExistingClient = Database.Clients.FirstOrDefault(x => x.Id == clientCommand.Id);
            if (alreadyExistingClient != null)
            {
                return BadRequest();
            }
            Database.Clients.Add(clientCommand);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientCommand clientCommand)
        {
            if (clientCommand is null)
            {
                return BadRequest();
            }
            if (clientCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == clientCommand.Id);
            if (existingClient == null)
            {
                return BadRequest();
            }
            var indefOfExistingClient = Database.Clients.IndexOf(existingClient);
            Database.Clients[indefOfExistingClient] = clientCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == id);
            if (existingClient is null)
            {
                return BadRequest();
            }
            Database.Clients.Remove(existingClient);
            return Ok();
        }
    }
}
