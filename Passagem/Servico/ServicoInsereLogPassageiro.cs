using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using Newtonsoft.Json;

namespace MicroServicoPassagemAereaAPI.Servico
{
    public class ServicoInsereLogPassagemAerea
    {
        public static async Task<string> InsereLogPassagemAerea(Log log)
        {
            HttpClient client = new HttpClient();
            try
            {
                var json = JsonConvert.SerializeObject(log);
                var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
                var resultado = await client.PostAsync("https://localhost:44320/api/Logs", conteudo);
                resultado.EnsureSuccessStatusCode();
                if (resultado.IsSuccessStatusCode)
                    return "ok";
                else
                    return "notOk";
            }
            catch (HttpRequestException exception)
            {
                return "notOk";
            }
        }
    }
}
