using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Newtonsoft.Json;

namespace ValidaCEP
{
    public class ApiCorreios
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<Endereco> ViacepJsonAsync(string cep)
        {
            Endereco enderecoErro = new();

            try
            {
                var resposta = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                resposta.EnsureSuccessStatusCode();
                string responseBody = await resposta.Content.ReadAsStringAsync();
                var endereco = JsonConvert.DeserializeObject<Endereco>(responseBody);
                return endereco;
            }
            catch (HttpRequestException excecao)
            {
                enderecoErro.Logradouro = excecao.Message;
                return enderecoErro;
            }
        }
    }
}
