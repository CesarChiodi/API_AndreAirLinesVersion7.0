using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServicoVooAPI.Configuracao;
using MicroServicoVooAPI.Servico;
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

namespace MicroServicoVooAPI
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Voo", Version = "v1" });
            });
            servico.Configure<VooAPI>(
               Configuracao.GetSection(nameof(VooAPI)));

            servico.AddSingleton<IVooAPI>(sp =>
                sp.GetRequiredService<IOptions<VooAPI>>().Value);

            servico.AddSingleton<ServicoVoo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Voo v1"));
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
