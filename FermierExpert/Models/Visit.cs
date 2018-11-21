using System;

namespace FermierExpert.Models
{
    public class Visit : BaseEntity
    {
        public DateTime Date { get; set; }
        public string VisitReason { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
    }
}
