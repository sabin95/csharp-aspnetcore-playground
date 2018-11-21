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
        [HttpGet("save")]
        public async Task SaveDatabase()
        {
            if (MyConfig.DisableSave)
            {
                return;
            }
            await serialize(Database.Clients, nameof(Database.Clients));
            await serialize(Database.Employees, nameof(Database.Employees));
            await serialize(Database.Companies, nameof(Database.Companies));
            await serialize(Database.Crops, nameof(Database.Crops));
            await serialize(Database.Products, nameof(Database.Products));
            await serialize(Database.Stocks, nameof(Database.Stocks));
            await serialize(Database.Visits, nameof(Database.Visits));
            await serialize(Database.CropFields, nameof(Database.CropFields));
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
