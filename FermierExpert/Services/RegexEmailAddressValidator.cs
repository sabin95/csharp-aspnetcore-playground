using FermierExpert.Services.Contracts;
using System.Text.RegularExpressions;

namespace FermierExpert.Services
{
    public class RegexEmailAddressValidator : IEmailAddressValidator
    {
        public bool IsEmailValid(string email)
        {
            var regex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            return Regex.IsMatch(email, regex);
        }
    }
}
