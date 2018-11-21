using FermierExpert.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Responses
{
    public class VisitResponse : Visit
    {
        public ClientResponse Client { get; set; }
        public EmployeeResponse Employee { get; set; }
        [JsonConstructor]
        public VisitResponse()
        {

        }
        public VisitResponse(Visit visit)
        {
            Id = visit.Id;
            Date = visit.Date;
            VisitReason = visit.VisitReason;
            EmployeeId = visit.EmployeeId;
            ClientId = visit.ClientId;
        }
    }
}
