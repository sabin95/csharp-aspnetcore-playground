using System;
using System.Linq;
using System.Threading.Tasks;
using FermierExpert.Commands;
using FermierExpert.Data;
using FermierExpert.Models;
using FermierExpert.Queries;
using FermierExpert.Responses;
using FermierExpert.Services.Contracts;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private IPhoneNumberValidator _phoneValidator;
        private IEmailAddressValidator _emailValidator;
        private Database _database;
        private readonly IQueryHelper _queryExtensions;
        public EmployeesController(Database database, IPhoneNumberValidator phoneValidator, 
                                    IEmailAddressValidator emailValidator, IQueryHelper queryExtension)
        {
            _database = database;
            _phoneValidator = phoneValidator;
            _emailValidator = emailValidator;
            _queryExtensions = queryExtension;
        }
        [HttpGet]
        public IActionResult Get(GetAllBaseQuery<EmployeeCommand> query)
        {
            var employeeResponse = new ListaDubluInlantuita<EmployeeResponse>();
            var filteredList = _queryExtensions.WhereByColumns(_database.Employees, query.FilterPayload);
            var orderedList = _queryExtensions
                .OrderByColumns(filteredList, query.SortColumns);
            var sortedList = _queryExtensions.Slice(orderedList, query.Start, query.Count);
            foreach (var employee in sortedList)
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
            _database.Employees.Remove(existingEmployee);
            return Ok();
        }
    }
}
