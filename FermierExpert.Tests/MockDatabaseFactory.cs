using FermierExpert.Data;
using FermierExpert.Models;
using ListaDubluInlantuita;

namespace FermierExpert.Tests
{
    public static class MockDatabaseFactory
    {
        public static Database CreateNewDatabase()
        {
            var db = new Database
            {
                Clients = new ListaDubluInlantuita<Client>
                {
                    new Client
                    {
                        Id = 1,
                        FirstName = "Alex",
                        LastName = "Stanescu",
                        DateOfBirth = new System.DateTime(1995,12,01),
                        Language = SpokenLanguage.English
                    },
                    new Client
                    {
                        Id = 2,
                        FirstName = "Mihai",
                        LastName = "Stanescu",
                        DateOfBirth = new System.DateTime(1995,10,01),
                        Language = SpokenLanguage.French
                    },
                    new Client
                    {
                        Id = 3,
                        FirstName = "Iosif",
                        LastName = "Sandu",
                        DateOfBirth = new System.DateTime(1995,08,01),
                        Language = SpokenLanguage.English
                    },
                     new Client
                    {
                        Id = 4,
                        FirstName = "Ilie",
                        LastName = "Dumitrescu",
                        DateOfBirth = new System.DateTime(1995,07,01),
                        Language = SpokenLanguage.English
                    },
                    new Client
                    {
                        Id = 5,
                        FirstName = "Ionut",
                        LastName = "Popescu",
                        DateOfBirth = new System.DateTime(1995,05,01),
                        Language = SpokenLanguage.German
                    },
                    new Client
                    {
                        Id = 6,
                        FirstName = "Elena",
                        LastName = "Vasile",
                        DateOfBirth = new System.DateTime(1995,11,01),
                        Language = SpokenLanguage.Italian
                    },
                },
                Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } },
                Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } },
                Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } },
                Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } },
                Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } },
                Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } },
                CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } }
            };
            return db;
        }
    }
}
