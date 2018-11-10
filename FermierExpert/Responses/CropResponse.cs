using FermierExpert.Models;
using Newtonsoft.Json;

namespace FermierExpert.Responses
{
    public class CropResponse : Crop
    {
        [JsonConstructor]
        public CropResponse()
        {

        }
        public CropResponse(Crop crop)
        {
            Id = crop.Id;
            Name = crop.Name;
        }
    }
}
