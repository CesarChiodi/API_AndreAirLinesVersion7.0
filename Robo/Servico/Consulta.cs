using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Modelo;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Robo.Servico
{
    public class Consulta
    {
        public static async Task RelatorioPassagemAerea(int mes)
        {
            using var client = new HttpClient();

            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44347/api/PassagemAerea");
                resposta.EnsureSuccessStatusCode();
                string responseBody = await resposta.Content.ReadAsStringAsync();


                var passagemAerea = JsonConvert.DeserializeObject<List<PassagemAerea>>(responseBody);

                var passagemAereaRelatorio =
                    (from passagem in passagemAerea
                     where passagem.DataCadastro.Month == mes
                     select passagem);

                foreach (var passagem in passagemAereaRelatorio)
                {
                    Console.WriteLine(passagem.ToString());
                }
                
                string nomeArquivo = "RelatorioPassagemAerea.json";            
                string stringJson = JsonSerializer.Serialize(passagemAereaRelatorio);
                File.WriteAllText(nomeArquivo, stringJson);

            }
            catch (Exception excecao)
            {
                throw new Exception(excecao.Message);
            }
        }

        public static async Task RelatorioPrecoBase()
        {
            using var client = new HttpClient();

            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44347/api/PrecoBase");
                resposta.EnsureSuccessStatusCode();
                string responseBody = await resposta.Content.ReadAsStringAsync();


                var precoBase = JsonConvert.DeserializeObject<List<PrecoBase>>(responseBody);

                var precoBaseRelatorio =
                    (from passagem in precoBase
                     select passagem);

                foreach (var preco in precoBaseRelatorio)
                {
                    Console.WriteLine(precoBase.ToString());
                }

                string nomeArquivo = "RelatorioPrecoBase.json";
                string stringJson = JsonSerializer.Serialize(precoBaseRelatorio);
                File.WriteAllText(nomeArquivo, stringJson);

            }
            catch (Exception excecao)
            {
                throw new Exception(excecao.Message);
            }
        }
    }
}

