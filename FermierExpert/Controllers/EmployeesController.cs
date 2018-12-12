using System;
using System.Linq;
using System.Threading.Tasks;
using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Models;
using FermierExpert.Responses;
using FermierExpert.Services.Contracts;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IPhoneNumberValidator _phoneValidator;
        private IEmailAddressValidator _emailValidator;
        private Database _database;
        public EmployeesController(Database database, IPhoneNumberValidator phoneValidator, IEmailAddressValidator emailValidator)
        {
            _database = database;
            _phoneValidator = phoneValidator;
            _emailValidator = emailValidator;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var employeeResponse = new ListaDubluInlantuita<EmployeeResponse>();
            foreach (var employee in _database.Employees)
            {
                var visits = new ListaDubluInlantuita<VisitResponse>();
                foreach (var visit in _database.Visits
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
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == id);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            var visits = new ListaDubluInlantuita<VisitResponse>();
            foreach (var visit in _database.Visits
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
        public IActionResult GetByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest("Name is null");
            }
            var employeeResponses = new ListaDubluInlantuita<EmployeeResponse>();
            foreach (var existingEmployee in _database.Employees
                .Where(x => x.LastName.ToLower().Contains((name.ToLower())) || x.FirstName.ToLower().Contains((name.ToLower())))
                .Select(x => new EmployeeResponse(x)))
            {
                var visits = new ListaDubluInlantuita<VisitResponse>();
                foreach (var visit in _database.Visits
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
        public async Task<IActionResult> Add([FromBody] EmployeeCommand employeeCommand)
        {
            if (employeeCommand is null)
            {
                return BadRequest();
            }
            if (employeeCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == employeeCommand.Id);
            if (existingEmployee != null)
            {
                return BadRequest();
            }
            if (!await _phoneValidator.IsPhoneNumberValid(employeeCommand.Phone))
            {
                return BadRequest("Invalid phone number");
            }
            if (!_emailValidator.IsEmailValid(employeeCommand.Email))
            {
                return BadRequest("Email is invalid.");
            }
            _database.Employees.Add(employeeCommand);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeCommand employeeCommand)
        {
            if (employeeCommand is null)
            {
                return BadRequest();
            }
            if (employeeCommand.Id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == employeeCommand.Id);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            if (!await _phoneValidator.IsPhoneNumberValid(employeeCommand.Phone))
            {
                return BadRequest("Invalid phone number");
            }
            if (!_emailValidator.IsEmailValid(employeeCommand.Email))
            {
                return BadRequest("Email is invalid.");
            }
            var indefOfExistingEmployee = _database.Employees.IndexOf(existingEmployee);
            _database.Employees[indefOfExistingEmployee] = employeeCommand;
            return Ok();

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingEmployee = _database.Employees.FirstOrDefault(x => x.Id == id);
            if (existingEmployee is null)
            {
                return BadRequest();
            }
            var existingVisit = _database.Visits.FirstOrDefault(x => x.EmployeeId == existingEmployee.Id);
            if (existingVisit != null)
            {
                return BadRequest();
            }
            _database.Employees.Remove(existingEmployee);
            return Ok();
        }
    }
}