using FermierExpert.Data;
using FermierExpert.Models;
using ListaDubluInlantuita;

namespace FermierExpert.Tests
{
    public static class MockDatabaseFactory
    {
        public static Database Create()
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
                Employees = new ListaDubluInlantuita<Employee>
                {
                    new Employee {
                        Id = 1,
                        FirstName = "Ioan",
                        LastName = "Mihai",
                        Email = "ioan@yahoo.com",
                        Phone = "+40505123456"
                    },
                    new Employee {
                        Id = 2,
                        FirstName = "Gigi",
                        LastName = "Negru",
                        Email = "gigi@yahoo.com",
                        Phone = "+40755123456"
                    },
                    new Employee {
                        Id = 3,
                        FirstName = "Paul",
                        LastName = "Ionescu",
                        Email = "paul@yahoo.com",
                        Phone = "+40505123753"
                    },
                    new Employee {
                        Id = 4,
                        FirstName = "Andrei",
                        LastName = "Hagi",
                        Email = "andrei@yahoo.com",
                        Phone = "+40505185456"
                    },
                    new Employee {
                        Id = 5,
                        FirstName = "Andreea",
                        LastName = "Vasile",
                        Email = "andrea@yahoo.com",
                        Phone = "+40505957456"
                    },
                    new Employee {
                        Id = 6,
                        FirstName = "Ana",
                        LastName = "Mihaela",
                        Email = "ana@yahoo.com",
                        Phone = "+40852123456"
                    },
                },
                Companies = new ListaDubluInlantuita<Company>
                {
                    new Company {
                        Id = 1,
                        Name = "Pepito",
                        Country = "Romania"
                    },
                    new Company {
                        Id = 2,
                        Name = "MegaImage",
                        Country = "SUA"
                    },
                    new Company {
                        Id = 3,
                        Name = "Microsoft",
                        Country = "Germania"
                    },
                    new Company {
                        Id = 4,
                        Name = "Cube",
                        Country = "Franta"
                    },
                    new Company {
                        Id = 5,
                        Name = "Zewa",
                        Country = "Romania"
                    },
                },
                Crops = new ListaDubluInlantuita<Crop>
                {
                    new Crop
                    {
                        Id = 1,
                        Name = "Tomato"
                    },
                    new Crop
                    {
                        Id = 2,
                        Name = "Carrot"
                    },
                    new Crop
                    {
                        Id = 3,
                        Name = "Potato"
                    },
                    new Crop
                    {
                        Id = 4,
                        Name = "Cabbage"
                    },
                },
                Products = new ListaDubluInlantuita<Product>
                {
                    new Product
                    {
                        Id = 1,
                        CompanyId = 1,
                        Name = "Mega Potato"
                    },
                    new Product
                    {
                        Id = 2,
                        CompanyId = 3,
                        Name = "Mega Potato"
                    },
                    new Product
                    {
                        Id = 3,
                        CompanyId = 2,
                        Name = "Mega Carrot"
                    },
                    new Product
                    {
                        Id = 4,
                        CompanyId = 1,
                        Name = "Mega Tomato"
                    },
                },
                Stocks = new ListaDubluInlantuita<Stock>
                {
                    new Stock
                    {
                        Id = 1,
                        ClientId = 1,
                        ProductId = 1,
                        Ammount = 12,
                    },
                    new Stock
                    {
                        Id = 2,
                        ClientId = 2,
                        ProductId = 3,
                        Ammount = 42,
                    },
                    new Stock
                    {
                        Id = 3,
                        ClientId = 3,
                        ProductId = 1,
                        Ammount = 10,
                    },
                },
                Visits = new ListaDubluInlantuita<Visit>
                {
                    new Visit {
                        Id = 1,
                        ClientId = 1,
                        EmployeeId = 1,
                        Date = new System.DateTime(1995,10,4)
                    },
                    new Visit {
                        Id = 2,
                        ClientId = 2,
                        EmployeeId = 2,
                        Date = new System.DateTime(1995,12,5)
                    },
                    new Visit {
                        Id = 3,
                        ClientId = 3,
                        EmployeeId = 2,
                        Date = new System.DateTime(1995,3,4)
                    },
                },
                CropFields = new ListaDubluInlantuita<CropField>
                {
                    new CropField
                    {
                        Id = 1,
                        ClientId = 1,
                        Name = "Brazda"
                    },
                    new CropField
                    {
                        Id = 2,
                        ClientId = 2,
                        Name = "Lucerna"
                    }
                }
            };
            return db;
        }
    }
}
