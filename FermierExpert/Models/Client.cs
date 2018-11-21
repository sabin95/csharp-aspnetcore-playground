using System;

namespace FermierExpert.Models
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public SpokenLanguage Language { get; set; }
    }
}
