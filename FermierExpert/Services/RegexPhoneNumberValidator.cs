using FermierExpert.Services.Contracts;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FermierExpert.Services
{
    public class RegexPhoneNumberValidator : IPhoneNumberValidator
    {
        public async Task<bool> IsPhoneNumberValid(string phoneNumber)
        {
            var regex = @"\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})? ?(\w{1,10}\s?\d{1,6})?";
            return await Task.FromResult(Regex.IsMatch(phoneNumber, regex));
        }
    }
}
