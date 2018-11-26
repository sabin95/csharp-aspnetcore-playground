using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Services.Contracts
{
    public interface IEmailAddressValidator
    {
        bool IsEmailValid(string email);
    }
}
