using System;
using System.Net.Http;
using System.Threading.Tasks;
using Modelo;
using Newtonsoft.Json;

namespace MicroServicoPassageiroAPI.Servico
{
    public class ServicoVerificaUsuarioPassageiro
    {
        public static async Task<Usuario> BuscaUsuarioPassageiro(string login)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44320/api/User/" + login);
                resposta.EnsureSuccessStatusCode();
                string responseBody = await resposta.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Usuario>(responseBody);
                return user;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
