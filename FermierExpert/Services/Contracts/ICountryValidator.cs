using FermierExpert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Services.Contracts
{
    public interface ICountryValidator
    {
        Task<RapidApiCountryResult[]> SearchCountry(string countryName);
    }
}
