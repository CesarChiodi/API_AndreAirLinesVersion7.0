using Microsoft.EntityFrameworkCore;

namespace AndreAirLinesFrontAPI.Models
{
    [Keyless]
    public class EnderecoMongo
    {
        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Pais { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string UF { get; set; }

        public int Numero { get; set; }

        public string Complemento { get; set; }

        public string Continente { get; set; }
    }
}
