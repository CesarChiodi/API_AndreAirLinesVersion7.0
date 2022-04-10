using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using Newtonsoft.Json;

namespace MicroServicoUsuarioAPI.Servico
{
    public class InsereLog
    {
        public static async Task<string> InsertLog(Log log)
        {
            HttpClient client = new HttpClient();

            try
            {
                var json = JsonConvert.SerializeObject(log);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://localhost:44320/api/Logs", content);
                result.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    return "ok";
                else
                    return "naoOk";
            }
            catch (HttpRequestException exception)
            {
                return "naoOk";
            }
        }

    }
}
