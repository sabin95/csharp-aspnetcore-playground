using FermierExpert.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Responses
{
    public class StockResponse : Stock
    {
        public ClientResponse Client { get; set; }
        public ProductResponse Product { get; set; }
        [JsonConstructor]
        public StockResponse()
        {

        }
        public StockResponse(Stock stock)
        {
            Id = stock.Id;
            ProductId = stock.ProductId;
            ClientId = stock.ClientId;
            Ammount = stock.Ammount;
        }
    }
}
