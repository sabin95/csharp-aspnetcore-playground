using FermierExpert.Models;
using ListaDubluInlantuita;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Responses
{
    public class ProductResponse : Product
    {
        public ListaDubluInlantuita<StockResponse> Stocks { get; set; }
        public CompanyResponse Company { get; set; }
        [JsonConstructor]
        public ProductResponse()
        {

        }
        public ProductResponse(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            CompanyId = product.CompanyId;
        }
    }
}
