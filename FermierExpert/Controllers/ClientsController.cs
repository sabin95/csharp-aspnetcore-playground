using FermierExpert.Data;
using FermierExpert.Models;
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
            return Ok(Database.Clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            return Ok(Database.Clients.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] Client client)
        {
            if (client is null)
            {
                return BadRequest();
            }
            if (client.Id <= 0)
            {
                return BadRequest();
            }
            var alreadyExistingClient = Database.Clients.FirstOrDefault(x => x.Id == client.Id);
            if (alreadyExistingClient != null)
            {
                return BadRequest();
            }
            Database.Clients.Add(client);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Client client)
        {
            if (client is null)
            {
                return BadRequest();
            }
            if (client.Id <= 0)
            {
                return BadRequest();
            }
            var existingClient = Database.Clients.FirstOrDefault(x => x.Id == client.Id);
            if (existingClient == null)
            {
                return BadRequest();
            }
            var indefOfExistingClient = Database.Clients.IndexOf(existingClient);
            Database.Clients[indefOfExistingClient] = client;
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
