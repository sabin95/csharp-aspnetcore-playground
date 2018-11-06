using FermierExpert.Models;
using ListaDubluInlantuita;

namespace FermierExpert.Responses
{
    public class ClientResponse : Client
    {
        public ListaDubluInlantuita<CropFieldResponse> Fields { get; set; }
        public ClientResponse()
        {

        }
        public ClientResponse(Client client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            LastName = client.LastName;
            DateOfBirth = client.DateOfBirth;
            Language = client.Language;
        }
    }
}
