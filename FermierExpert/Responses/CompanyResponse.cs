using FermierExpert.Models;
using ListaDubluInlantuita;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Responses
{
    public class CompanyResponse : Company
    {
        public ListaDubluInlantuita<ProductResponse> Products { get; set; }
        [JsonConstructor]
        public CompanyResponse()
        {

        }
        public CompanyResponse(Company company)
        {
            Id = company.Id;
            Name = company.Name;
            Country = company.Country;
        }
    }
}
