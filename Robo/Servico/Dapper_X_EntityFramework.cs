using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsultaDapperSQL.Modelo;
using IngestaoDadosAeroporto.Modelo;
using Newtonsoft.Json;

namespace Robo.Servico
{
    public class Dapper_X_EntityFramework
    {

        public static async Task ValidaPerformanceDapper(int id)
        {
            HttpClient client = new HttpClient();


            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44390/api/AeroportoCD" + id);
                resposta.EnsureSuccessStatusCode();

                var responseBody = await resposta.Content.ReadAsStringAsync();
                var aeroportos = JsonConvert.DeserializeObject<AeroportoCD>(responseBody);

                Console.WriteLine(aeroportos + "\n");
                Console.WriteLine(DateTime.Now + "\n");
            }
            catch (HttpRequestException excecao)
            {
                throw new HttpRequestException(excecao.Message);
            }
        }


        public async Task ValidaPerformanceEntityFramework(int id)
        {

            HttpClient client = new HttpClient();


            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44377/api/Aeroporto" + id);
                resposta.EnsureSuccessStatusCode();

                var responseBody = await resposta.Content.ReadAsStringAsync();
                var aeroportos = JsonConvert.DeserializeObject<Aeroporto>(responseBody);

                Console.WriteLine(aeroportos + "\n");
                Console.WriteLine(DateTime.Now + "\n");
            }
            catch (HttpRequestException excecao)
            {
                throw new HttpRequestException(excecao.Message);
            }
        }
        public void Comparacao()
        {
            DateTime fim = DateTime.Now, inicio = DateTime.Now;

            for (int id = 1; id <= 500; id++)
            {

                ValidaPerformanceDapper(id).Wait();

                if (id == 1)
                {
                    inicio = DateTime.Now;
                }

                if (id == 500)
                {
                    fim = DateTime.Now;
                }
            }

            var tempoDapper = fim - inicio;

            for (int id = 1; id <= 500; id++)
            {

                ValidaPerformanceEntityFramework(id).Wait();


                if (id == 1)
                {
                    inicio = DateTime.Now;
                }

                if (id == 500)
                {
                    fim = DateTime.Now;
                }

            }

            var tempoEntityFramework = fim - inicio;


            Console.WriteLine("Tempo Dapper: " + tempoDapper);
            Console.WriteLine("Tempo Entity Framework: " + tempoEntityFramework);

            if (tempoEntityFramework > tempoDapper)
                Console.WriteLine("Dapper e mais performatico!");
            else if (tempoEntityFramework < tempoDapper)
                Console.WriteLine("Entity Framework e mais performatico!");
            else if (tempoEntityFramework == tempoDapper)
                Console.WriteLine("Performance permaneceu igual!");
        }
    }
}