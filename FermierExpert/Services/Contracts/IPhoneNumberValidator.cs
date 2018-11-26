using System.Threading.Tasks;

namespace FermierExpert.Services.Contracts
{
    public interface IPhoneNumberValidator
    {
        Task<bool> IsPhoneNumberValid(string phoneNumber);
    }
}
