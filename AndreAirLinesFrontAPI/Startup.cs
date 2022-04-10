using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AndreAirLinesFrontAPI.Data;
using AndreAirLinesFrontAPI.Configuracao;
using Microsoft.Extensions.Options;
using AndreAirLinesFrontAPI.Servico;

namespace AndreAirLinesFrontAPI
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
            servico.AddControllersWithViews();

            servico.AddDbContext<AndreAirLinesFrontAPIContext>(options =>
                    options.UseSqlServer(Configuracao.GetConnectionString("AndreAirLinesFrontAPIContext")));
            servico.Configure<AeroportoApiFront>(
               Configuracao.GetSection(nameof(AeroportoApiFront)));

            servico.AddSingleton<IAeroportoApiFront>(sp =>
                sp.GetRequiredService<IOptions<AeroportoApiFront>>().Value);

            servico.AddSingleton<ServicoAeroportoFront>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
