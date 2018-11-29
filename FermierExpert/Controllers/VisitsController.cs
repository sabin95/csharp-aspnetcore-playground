using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Models;
using FermierExpert.Responses;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private Database _database;
        public VisitsController(Database database)
        {
            _database = database;
        }
        [HttpGet("client/{clientId}")]
        public IActionResult GetVisitsByClient(int clientId)
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
            var visitsResponse = new ListaDubluInlantuita<VisitResponse>();
            foreach (var visit in _database.Visits
                .Where(x => x.ClientId == existingClient.Id)
                .Select(x => new VisitResponse(x)))
            {
                visit.Client = new ClientResponse(existingClient);
                var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == visit.EmployeeId);
                if (existingEmployee != null)
                {
                    visit.Employee = new EmployeeResponse(existingEmployee);
                }
                visitsResponse.Add(visit);
            }
            return Ok(visitsResponse);
        }

        [HttpGet("employee/{employeeId}")]
        public IActionResult GetVisitsByEmployee(int employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == employeeId);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            var visitsResponse = new ListaDubluInlantuita<VisitResponse>();
            foreach (var visit in _database.Visits
                .Where(x => x.EmployeeId == existingEmployee.Id)
                .Select(x => new VisitResponse(x)))
            {
                visit.Employee = new EmployeeResponse(existingEmployee);
                var existingClient = _database.Clients.FirstOrDefault(x => x.Id == visit.ClientId);
                if (existingClient != null)
                {
                    visit.Client = new ClientResponse(existingClient);
                }
                visitsResponse.Add(visit);
            }
            return Ok(visitsResponse);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingVisit = _database.Visits.FirstOrDefault(x => x.Id == id);
            if (existingVisit is null)
            {
                return BadRequest();
            }
            var response = new VisitResponse(existingVisit);
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == response.ClientId);
            if (existingClient != null)
            {
                response.Client = new ClientResponse(existingClient);
            }
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == response.EmployeeId);
            if (existingEmployee != null)
            {
                response.Employee = new EmployeeResponse(existingEmployee);
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] VisitCommand visitCommand)
        {
            if (visitCommand is null)
            {
                return BadRequest();
            }
            if (visitCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingVisit = _database.Visits.FirstOrDefault(x => x.Id == visitCommand.Id);
            if (existingVisit != null)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == visitCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == visitCommand.EmployeeId);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            _database.Visits.Add(visitCommand);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] VisitCommand visitCommand)
        {
            if (visitCommand is null)
            {
                return BadRequest();
            }
            if (visitCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingVisit = _database.Visits.FirstOrDefault(x => x.Id == visitCommand.Id);
            if (existingVisit is null)
            {
                return BadRequest();
            }
            var existingClient = _database.Clients.FirstOrDefault(x => x.Id == visitCommand.ClientId);
            if (existingClient is null)
            {
                return BadRequest();
            }
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == visitCommand.EmployeeId);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            var indexOfExistingVisit = _database.Visits.IndexOf(existingVisit);
            _database.Visits[indexOfExistingVisit] = visitCommand;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingVisit = _database.Visits.FirstOrDefault(x => x.Id == id);
            if (existingVisit is null)
            {
                return BadRequest();
            }
            _database.Visits.Remove(existingVisit);
            return Ok();
        }

    }
}