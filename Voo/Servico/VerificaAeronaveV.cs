using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Modelo;
using Newtonsoft.Json;

namespace MicroServicoVooAPI.Servico
{
    public class VerificaAeronaveV
    {
        public static async Task<Aeronave> EncontraAeronaveV(Aeronave aeronaveBusca)
        {
            HttpClient client = new HttpClient();
            Aeronave aronaveErro = new();

            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44310/api/Aeronave");
                resposta.EnsureSuccessStatusCode();

                var responseBody = await resposta.Content.ReadAsStringAsync();
                var aeronave = JsonConvert.DeserializeObject<List<Aeronave>>(responseBody);

                var consulta = (from aeronaveConsulta in aeronave
                                where aeronaveConsulta.IdAeronave == aeronaveBusca.IdAeronave
                                select aeronaveConsulta).FirstOrDefault();

                return consulta;

               

            }
            catch (HttpRequestException excecao)
            {
                aronaveErro.NomeAeronave = excecao.Message;
                return aronaveErro;
            }
        }
    }
}
