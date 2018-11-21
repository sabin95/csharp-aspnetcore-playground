using FermierExpert.Models;
using ListaDubluInlantuita;

namespace FermierExpert.Data
{
    public class Database
    {
        public static ListaDubluInlantuita<Client> Clients { get; set; } = new ListaDubluInlantuita<Client>();
        public static ListaDubluInlantuita<CropField> CropFields { get; set; } = new ListaDubluInlantuita<CropField>();
        public static ListaDubluInlantuita<Crop> Crops { get; set; } = new ListaDubluInlantuita<Crop>();
        public static ListaDubluInlantuita<Company> Companies { get; set; } = new ListaDubluInlantuita<Company>();
        public static ListaDubluInlantuita<Employee> Employees { get; set; } = new ListaDubluInlantuita<Employee>();
        public static ListaDubluInlantuita<Product> Products { get; set; } = new ListaDubluInlantuita<Product>();
        public static ListaDubluInlantuita<Stock> Stocks { get; set; } = new ListaDubluInlantuita<Stock>();
        public static ListaDubluInlantuita<Visit> Visits { get; set; } = new ListaDubluInlantuita<Visit>();

    }
}
