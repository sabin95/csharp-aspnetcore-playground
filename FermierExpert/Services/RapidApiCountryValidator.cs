using FermierExpert.Models;
using FermierExpert.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FermierExpert.Services
{
    public class RapidApiCountryValidator : ICountryValidator
    {
        public async Task<RapidApiCountryResult[]> SearchCountry(string countryName)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "7582e41fc2msh00efa97b8ac9c16p137a4fjsn78995d34c8c0");
            var result = await httpClient.GetAsync("https://restcountries-v1.p.rapidapi.com/name/" + countryName);
            


            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var deserializedResponse = JsonConvert.DeserializeObject<RapidApiCountryResult[]>(response);
                return deserializedResponse;
            }

            return new RapidApiCountryResult[] { };
        }
    }
}
