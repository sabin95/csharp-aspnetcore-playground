﻿using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Queries;
using FermierExpert.Responses;
using FermierExpert.Services;
using FermierExpert.Services.Contracts;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly Database _database;
        private readonly IQueryHelper _queryExtensions;

        public ClientsController(Database database, IQueryHelper queryExtensions)
        {
            _database = database;
            _queryExtensions = queryExtensions;
        }



        [HttpGet]
        public IActionResult GetAll(GetAllBaseQuery<ClientCommand> query)
        {

            var clientsResponse = new ListaDubluInlantuita<ClientResponse>();
            var filteredList = _queryExtensions.WhereByColumns(_database.Clients, query.FilterPayload);
            var orderedList = _queryExtensions
                .OrderByColumns(filteredList, query.SortColumns);
            var sortedList = _queryExtensions.Slice(orderedList, query.Start, query.Count);


            foreach (var client in sortedList)
            {
                var cropFields = new ListaDubluInlantuita<CropFieldResponse>();
                foreach (var cropField in _database.CropFields
                .Where(x => x.ClientId == client.Id)
                .Select(x => new CropFieldResponse(x)))
                {
                    cropFields.Add(cropField);
                }
                var visits = new ListaDubluInlantuita<VisitResponse>();
                foreach (var visit in _database.Visits
                .Where(x => x.ClientId == client.Id)
                .Select(x => new VisitResponse(x)))
                {
                    visits.Add(visit);
                }
                var stocks = new ListaDubluInlantuita<StockResponse>();
                foreach (var stock in _database.Stocks
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
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == id);
            if (existingClient is null)
            {
                return BadRequest();
            }

            var cropFields = new ListaDubluInlantuita<CropFieldResponse>();
            foreach (var cropField in _database.CropFields
                .Where(x => x.ClientId == id)
                .Select(x => new CropFieldResponse(x)))
            {
                cropFields.Add(cropField);
            }
            var visits = new ListaDubluInlantuita<VisitResponse>();
            foreach (var visit in _database.Visits
            .Where(x => x.ClientId == existingClient.Id)
            .Select(x => new VisitResponse(x)))
            {
                visits.Add(visit);
            }
            var stocks = new ListaDubluInlantuita<StockResponse>();
            foreach (var stock in _database.Stocks
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
            var alreadyExistingClient = _database.Clients.FirstOrDefault(x => x.Id == clientCommand.Id);
            if (alreadyExistingClient != null)
            {
                return BadRequest();
            }
            _database.Clients.Add(clientCommand);
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
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == clientCommand.Id);
            if (existingClient == null)
            {
                return BadRequest();
            }
            var indefOfExistingClient = _database.Clients.IndexOf(existingClient);
            _database.Clients[indefOfExistingClient] = clientCommand;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == id);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var existingCropField = _database.CropFields.FirstOrDefault(x => x.ClientId == existingClient.Id);
            if (existingCropField != null)
            {
                return BadRequest();
            }
            var existingStock = _database.Stocks.FirstOrDefault(x => x.ClientId == existingClient.Id);
            if (existingStock != null)
            {
                return BadRequest();
            }
            var existingVisit = _database.Visits.FirstOrDefault(x => x.ClientId == existingClient.Id);
            if (existingVisit != null)
            {
                return BadRequest();
            }
            _database.Clients.Remove(existingClient);
            return Ok();
        }
    }
}
