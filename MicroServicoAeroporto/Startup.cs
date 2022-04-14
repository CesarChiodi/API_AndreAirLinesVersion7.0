using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServicoAeronaveAPI.Configuracao;
using MicroServicoAeroportoAPI.Configuracao;
using MicroServicoAeroportoAPI.Servico;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MicroServicoAeroportoAPI
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
            servico.AddCors();
            servico.AddControllers();
            var key = Encoding.ASCII.GetBytes(ClasseConfiguracao.Secret);
            servico.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroServicoAeroporto", Version = "v1" });
            });
            servico.AddAuthentication(autenticacao =>
            {
                autenticacao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                autenticacao.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(autenticacao =>
            {
                autenticacao.RequireHttpsMetadata = false;
                autenticacao.SaveToken = true;
                autenticacao.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            servico.Configure<AeroportoAPI>(
               Configuracao.GetSection(nameof(AeroportoAPI)));

            servico.AddSingleton<IAeroportoAPI>(sp =>
                sp.GetRequiredService<IOptions<AeroportoAPI>>().Value);

            servico.AddSingleton<ServicoAeroporto>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroServicoAeroporto v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(autenticacao => autenticacao
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
