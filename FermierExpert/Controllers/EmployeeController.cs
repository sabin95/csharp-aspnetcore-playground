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
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var employeeResponse = new ListaDubluInlantuita<EmployeeResponse>();
            foreach (var employee in Database.Employees)
            {
                var visits = new ListaDubluInlantuita<VisitResponse>();
                foreach (var visit in Database.Visits
                        .Where(x => x.EmployeeId == employee.Id)
                        .Select(x => new VisitResponse(x)))
                {
                    visits.Add(visit);
                }
                var response = new EmployeeResponse(employee)
                {
                    Visits = visits
                };
                employeeResponse.Add(response);
            };
            return Ok(employeeResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = Database.Employees.FirstOrDefault(x => x.Id == id);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            var visits = new ListaDubluInlantuita<VisitResponse>();
            foreach (var visit in Database.Visits
                .Where(x => x.EmployeeId == existingEmployee.Id)
                .Select(x => new VisitResponse(x)))
            {
                visits.Add(visit);
            }
            var response = new EmployeeResponse(existingEmployee)
            {
                Visits = visits
            };
            return Ok(response);
        }

        [HttpGet("search/{name}")]
        public IActionResult GetByName (string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest("Name is null");
            }
            var employeeResponses = new ListaDubluInlantuita<EmployeeResponse>();
            foreach (var existingEmployee in Database.Employees
                .Where(x => x.LastName.ToLower().Contains((name.ToLower())) || x.FirstName.ToLower().Contains((name.ToLower())))
                .Select(x => new EmployeeResponse(x)))
            {
                var visits = new ListaDubluInlantuita<VisitResponse>();
                foreach (var visit in Database.Visits
                    .Where(x => x.EmployeeId == existingEmployee.Id)
                    .Select(x => new VisitResponse(x)))
                {
                    visits.Add(visit);
                }
                var response = new EmployeeResponse(existingEmployee)
                {
                    Visits = visits
                };
                employeeResponses.Add(existingEmployee);
            }
            return Ok(employeeResponses);
        }

       

        [HttpPost]
        public IActionResult Add([FromBody] EmployeeCommand employee)
        {
            if (employee is null)
            {
                return BadRequest();
            }
            if (employee.Id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = Database.Employees.FirstOrDefault(x => x.Id == employee.Id);
            if (existingEmployee != null)
            {
                return BadRequest();
            }
            Database.Employees.Add(employee);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] EmployeeCommand employeeCommand)
        {
            if (employeeCommand is null)
            {
                return BadRequest();
            }
            if (employeeCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = Database.Employees.FirstOrDefault(x => x.Id == employeeCommand.Id);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            var indefOfExistingEmployee = Database.Employees.IndexOf(existingEmployee);
            Database.Employees[indefOfExistingEmployee] = employeeCommand;
            return Ok();

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = Database.Employees.FirstOrDefault(x => x.Id == id);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            Database.Employees.Remove(existingEmployee);
            return Ok();
        }

    }
}