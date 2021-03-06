using System.Text;
using MicroServicoPrecoBaseAPI.Configuraao;
using MicroServicoPrecoBaseAPI.Configuracao;
using MicroServicoPrecoBaseAPI.Servico;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MicroServicoPrecoBaseAPI
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

            var key = Encoding.ASCII.GetBytes(ClasseConfiguracaoPrecoBase.Secret);

            servico.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrecoBase", Version = "v1" });
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
            servico.Configure<PrecoBaseAPI>(
               Configuracao.GetSection(nameof(PrecoBaseAPI)));

            servico.AddSingleton<IPrecoBaseAPI>(sp =>
                sp.GetRequiredService<IOptions<PrecoBaseAPI>>().Value);

            servico.AddSingleton<ServicoPrecoBase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrecoBase v1"));
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
