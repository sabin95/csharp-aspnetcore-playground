using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Models
{
    public class NumverifyValidationResult
    {
        public bool Valid { get; set; }
        public long Number { get; set; }
        public string LocalFormat { get; set; }
        public string InternationalFormat { get; set; }
        public string CountryPrefix { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Location { get; set; }
        public string Carrier { get; set; }
        public object LineType { get; set; }
    }
}
