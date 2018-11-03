using ListaDubluInlantuita;
using System;

namespace FermierExpert.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public SpokenLanguage Language { get; set; }
        public ListaDubluInlantuita<CropField> Fields { get; set; }
    }
}
