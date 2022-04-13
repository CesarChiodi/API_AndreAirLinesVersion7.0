using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Modelo;

namespace Robo.Servico
{
    public static class Inserir
    {
        //referencia proxy       

        /* IWebProxy _proxyConfig = new WebProxy
         {
             Address = new Uri($"{proxyIp}:{proxyPort}"),
             BypassProxyOnLocal = false,
             UseDefaultCredentials = true,
         };
         HttpClientHandler httpClientHandler = new HttpClientHandler
         {
             Proxy = _proxyConfig,
         };
         using (HttpClient _client = new HttpClient(handler: httpClientHandler)) {...}*/


        static readonly HttpClient client = new HttpClient();
        public static async Task Insertion()
        {
            client.BaseAddress = new Uri("https://localhost:44347/");
            string pathFileVoo = @"C:\Users\Cesar C Siqueira\OneDrive\Documentos\generated.json";
            List<Root> lista = ReadFile.GetData(pathFileVoo);
            foreach (var item in lista)
            {
                HttpResponseMessage resposta = await client.PostAsJsonAsync("api/Voos", item);
                //_context.Voo.Add(item);
            }
        }
    }
}

