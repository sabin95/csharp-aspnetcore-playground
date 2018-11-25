using FermierExpert.Data;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Controllers
{
    [Route("api/[controller]")]
    public class MasterController
    {
        private readonly Database _database;

        public MasterController(Database database)
        {
            _database = database;
        }
        [HttpGet("save")]
        public async Task SaveDatabase()
        {
            if (MyConfig.DisableSave)
            {
                return;
            }
            await serialize(_database.Clients, nameof(Database.Clients));
            await serialize(_database.Employees, nameof(Database.Employees));
            await serialize(_database.Companies, nameof(Database.Companies));
            await serialize(_database.Crops, nameof(Database.Crops));
            await serialize(_database.Products, nameof(Database.Products));
            await serialize(_database.Stocks, nameof(Database.Stocks));
            await serialize(_database.Visits, nameof(Database.Visits));
            await serialize(_database.CropFields, nameof(Database.CropFields));
        }

        private async Task serialize<T>(ListaDubluInlantuita<T> list, string filename)
        {
            if (list is null || !list.Any())
            {
                return;
            }
            var serialized = JsonConvert.SerializeObject(list);
            using (var sw = new StreamWriter($@"Database_State\{filename}.json"))
            {
                await sw.WriteAsync(serialized);
            }
        }
    }
}
