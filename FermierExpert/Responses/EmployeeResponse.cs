using FermierExpert.Models;
using ListaDubluInlantuita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Responses
{
    public class EmployeeResponse : Employee
    {
        public ListaDubluInlantuita<VisitResponse> Visits { get; set; }
        public EmployeeResponse()
        {
                
        }
        public EmployeeResponse(Employee employee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Email = employee.Email;
            Phone = employee.Phone;
        }
    }
}
