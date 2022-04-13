using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Modelo;
using Newtonsoft.Json;

namespace MicroServicoVooAPI.Servico
{
    public class VerificaAeroportoV         
    {
        public static async Task<Aeronave> EncontraAeroportoV(Aeronave aeroportoBusca)
        {
            HttpClient client = new HttpClient();
            //Aeroporto aeroporto = new Aeroporto();

            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44385/api/Aeroporto");
                resposta.EnsureSuccessStatusCode();

                var responseBody = await resposta.Content.ReadAsStringAsync();
                var aeroporto = JsonConvert.DeserializeObject <List<Aeronave>>(responseBody);

                var consulta = (from aeroportoConsulta in aeroporto
                                where aeroportoConsulta.IdAeronave == aeroportoBusca.IdAeronave
                                select aeroportoConsulta).FirstOrDefault();

                return consulta;

                //    HttpResponseMessage resposta = await client.GetAsync("https://localhost:44327/api/Aeroporto/" + aeroportoBusca.IdAeroporto);
                //    resposta.EnsureSuccessStatusCode();

                //    var responseBody = await resposta.Content.ReadAsStringAsync();
                //    aeroporto = JsonConvert.DeserializeObject<Aeroporto>(responseBody);


                //    //aeroporto.IdAeroporto = resposta.StatusCode.ToString();
                //    return aeroporto;

            }
            catch (HttpRequestException excecao)
            {
                throw;
            }
        }
    }
}
