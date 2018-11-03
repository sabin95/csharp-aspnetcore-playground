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
            await serialize(Database.Crops, nameof(Database.Crops));
            await serialize(Database.CropFields, nameof(Database.CropFields));
        }

        private async Task serialize<T>(ListaDubluInlantuita<T> list, string filename)
        {
            if (list is null || !list.Any())
            {
                return;
            }
            var serialized = JsonConvert.SerializeObject(list);
            using (var sw = new StreamWriter($"{filename}.json"))
            {
                await sw.WriteAsync(serialized);
            }
        }
    }
}
