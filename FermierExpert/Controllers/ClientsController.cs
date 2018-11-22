using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System;
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
                var visits = new ListaDubluInlantuita<VisitResponse>();
                foreach (var visit in Database.Visits
                .Where(x => x.ClientId == client.Id)
                .Select(x => new VisitResponse(x)))
                {
                    visits.Add(visit);
                }
                var stocks = new ListaDubluInlantuita<StockResponse>();
                foreach (var stock in Database.Stocks
                .Where(x => x.ClientId == client.Id)
                .Select(x => new StockResponse(x)))
                {
                    stocks.Add(stock);
                }
                var response = new ClientResponse(client)
                {
                    Fields = cropFields,
                    Stocks = stocks,
                    Visits = visits
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
            var visits = new ListaDubluInlantuita<VisitResponse>();
            foreach (var visit in Database.Visits
            .Where(x => x.ClientId == existingClient.Id)
            .Select(x => new VisitResponse(x)))
            {
                visits.Add(visit);
            }
            var stocks = new ListaDubluInlantuita<StockResponse>();
            foreach (var stock in Database.Stocks
            .Where(x => x.ClientId == existingClient.Id)
            .Select(x => new StockResponse(x)))
            {
                stocks.Add(stock);
            }
            var response = new ClientResponse(existingClient)
            {
                Fields = cropFields,
                Stocks = stocks,
                Visits = visits
            };
            return Ok(response);
        }

        [HttpGet("search/{name}")]
        public IActionResult GetByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest("Name is null");
            }
            var clientResponses = new ListaDubluInlantuita<ClientResponse>();
            foreach (var client in Database.Clients
                .Where(x=>x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower()))
                .Select(x=> new ClientResponse(x)))
            {
                var cropFields = new ListaDubluInlantuita<CropFieldResponse>();
                foreach (var cropField in Database.CropFields
                    .Where(x => x.ClientId == client.Id)
                    .Select(x => new CropFieldResponse(x)))
                {
                    cropFields.Add(cropField);
                }
                var visits = new ListaDubluInlantuita<VisitResponse>();
                foreach (var visit in Database.Visits
                .Where(x => x.ClientId == client.Id)
                .Select(x => new VisitResponse(x)))
                {
                    visits.Add(visit);
                }
                var stocks = new ListaDubluInlantuita<StockResponse>();
                foreach (var stock in Database.Stocks
                .Where(x => x.ClientId == client.Id)
                .Select(x => new StockResponse(x)))
                {
                    stocks.Add(stock);
                }
                var response = new ClientResponse(client)
                {
                    Fields = cropFields,
                    Stocks = stocks,
                    Visits = visits
                };
                clientResponses.Add(response);
            }
            return Ok(clientResponses);
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
