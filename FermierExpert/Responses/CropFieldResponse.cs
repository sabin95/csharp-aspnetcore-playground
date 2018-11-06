using FermierExpert.Models;
using Newtonsoft.Json;

namespace FermierExpert.Responses
{
    public class CropFieldResponse : CropField
    {
        public CropResponse Crop { get; set; }
        public ClientResponse Client { get; set; }
        [JsonConstructor]
        public CropFieldResponse()
        {

        }
        public CropFieldResponse(CropField cropField)
        {
            Id = cropField.Id;
            Size = cropField.Size;
            Name = cropField.Name;
            CropId = cropField.CropId;
            ClientId = cropField.ClientId;
        }
    }
}
