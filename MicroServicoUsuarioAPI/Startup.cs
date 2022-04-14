using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServicoUsuarioAPI.Configuracao;
using MicroServicoUsuarioAPI.Servico;
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

namespace MicroServicoUsuarioAPI
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

            var key = Encoding.ASCII.GetBytes(ClasseConfiguracaoUsuario.Secret);

            servico.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroServicoUsuarioAPI", Version = "v1" });
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
            servico.Configure<UsuarioAPI>(
               Configuracao.GetSection(nameof(UsuarioAPI)));

            servico.AddSingleton<IUsuarioAPI>(sp =>
                sp.GetRequiredService<IOptions<UsuarioAPI>>().Value);

            servico.AddSingleton<ServicoUsuario>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroServicoUsuarioAPI v1"));
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
