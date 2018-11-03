using FermierExpert.Data;
using FermierExpert.Models;
using ListaDubluInlantuita;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace FermierExpert
{
    public class MyConfig
    {
        public static bool DisableSave { get; } = false;
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.CustomSchemaIds(x => x.FullName);
                    c.SwaggerDoc("v1", new Info { Title = "FermierExpert API", Version = "v1" });
                })
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!MyConfig.DisableSave)
            {
                foreach (var property in typeof(Database).GetProperties())
                {
                    var filename = $"{property.Name}.json";
                    if (!File.Exists(filename))
                    {
                        continue;
                    }
                    using (var sr = new StreamReader(filename))
                    {
                        var data = JsonConvert.DeserializeObject(sr.ReadToEnd(), property.PropertyType);
                        property.SetValue(null, data);
                    }
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseSwagger()
                        .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1"); })
                        .UseMvc();
        }
    }
}
