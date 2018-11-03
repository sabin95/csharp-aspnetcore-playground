using FermierExpert.Models;
using ListaDubluInlantuita;

namespace FermierExpert.Data
{
    public class Database
    {
        public static ListaDubluInlantuita<Client> Clients { get; set; } = new ListaDubluInlantuita<Client>();
        public static ListaDubluInlantuita<CropField> CropFields { get; set; } = new ListaDubluInlantuita<CropField>();
        public static ListaDubluInlantuita<Crop> Crops { get; set; } = new ListaDubluInlantuita<Crop>();
    }
}
