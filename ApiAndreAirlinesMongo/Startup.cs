using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAndreAirlinesMongo.Configuracao;
using ApiAndreAirlinesMongo.Servico;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ApiAndreAirlinesMongo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuracao = configuration;
        }

        public IConfiguration Configuracao { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection servico)
        {

            servico.AddControllers();
            servico.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiAndreAirlinesMongo", Version = "v1" });
            });
            servico.Configure<InsereLogMongo>(
               Configuracao.GetSection(nameof(InsereLogMongo)));

            servico.AddSingleton<IInsereLogMongo>(sp =>
                sp.GetRequiredService<IOptions<InsereLogMongo>>().Value);

            servico.AddSingleton<ServicoAPIInsereMongo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiAndreAirlinesMongo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
