using FermierExpert.Data;
using FermierExpert.Models;
using ListaDubluInlantuita;
using System;
using System.Collections.Generic;
using System.Text;

namespace FermierExpert.Tests
{
    public static class MockDatabase
    {
        public static Database CreateNewDatabase()
        {
            return new Database
            {
                Clients = new ListaDubluInlantuita<Client> { new Client { Id = 1 } },
                Employees = new ListaDubluInlantuita<Employee> { new Employee { Id = 1 } },
                Companies = new ListaDubluInlantuita<Company> { new Company { Id = 1 } },
                Crops = new ListaDubluInlantuita<Crop> { new Crop { Id = 1 } },
                Products = new ListaDubluInlantuita<Product> { new Product { Id = 1, CompanyId = 1 } },
                Stocks = new ListaDubluInlantuita<Stock> { new Stock { Id = 1, ClientId = 1, ProductId = 1 } },
                Visits = new ListaDubluInlantuita<Visit> { new Visit { Id = 1, ClientId = 1, EmployeeId = 1 } },
                CropFields = new ListaDubluInlantuita<CropField> { new CropField { Id = 1, ClientId = 1 } }
            };
        }
    }
}
