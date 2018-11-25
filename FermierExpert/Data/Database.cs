using FermierExpert.Models;
using ListaDubluInlantuita;

namespace FermierExpert.Data
{
    public class Database
    {
        public ListaDubluInlantuita<Client> Clients { get; set; } = new ListaDubluInlantuita<Client>();
        public ListaDubluInlantuita<CropField> CropFields { get; set; } = new ListaDubluInlantuita<CropField>();
        public ListaDubluInlantuita<Crop> Crops { get; set; } = new ListaDubluInlantuita<Crop>();
        public ListaDubluInlantuita<Company> Companies { get; set; } = new ListaDubluInlantuita<Company>();
        public ListaDubluInlantuita<Employee> Employees { get; set; } = new ListaDubluInlantuita<Employee>();
        public ListaDubluInlantuita<Product> Products { get; set; } = new ListaDubluInlantuita<Product>();
        public ListaDubluInlantuita<Stock> Stocks { get; set; } = new ListaDubluInlantuita<Stock>();
        public ListaDubluInlantuita<Visit> Visits { get; set; } = new ListaDubluInlantuita<Visit>();

    }
}
