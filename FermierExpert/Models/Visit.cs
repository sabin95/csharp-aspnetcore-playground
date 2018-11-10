using System;

namespace FermierExpert.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string VisitReason { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
    }
}
