using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Modelo;
using Newtonsoft.Json;

namespace MicroServicoPassagemAereaAPI.Servico
{
    public class VerificaPassageiroPA
    {
        public static async Task<Passageiro> EncontraPassageiroPA(Passageiro passageiroBusca)
        {
            HttpClient client = new HttpClient();
            Passageiro passageiroErro = new();

            try
            {
                HttpResponseMessage resposta = await client.GetAsync("https://localhost:44385/api/Passageiro");
                resposta.EnsureSuccessStatusCode();

                var responseBody = await resposta.Content.ReadAsStringAsync();
                var passageiro = JsonConvert.DeserializeObject<List<Passageiro>>(responseBody);

                var consulta = (from passageiroConsulta in passageiro
                                where passageiroConsulta.Id == passageiroBusca.Id
                                select passageiroConsulta).FirstOrDefault();

                return consulta;



            }
            catch (HttpRequestException excecao)
            {
                passageiroErro.Nome = excecao.Message;
                return passageiroErro;
            }
        }
    }
}
