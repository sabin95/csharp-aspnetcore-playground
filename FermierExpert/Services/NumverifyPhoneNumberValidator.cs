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
    public class NumverifyPhoneNumberValidator : IPhoneNumberValidator
    {
        public async Task<bool> IsPhoneNumberValid(string phoneNumber)
        {
            var result = await new HttpClient().GetAsync("http://apilayer.net/api/validate?access_key=15fd53d18401381f7520bb6792bd35d1&number=" + phoneNumber);
            var response = await result.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<NumverifyValidationResult>(response);
            return deserializedResponse.Valid;
        }
    }
}
