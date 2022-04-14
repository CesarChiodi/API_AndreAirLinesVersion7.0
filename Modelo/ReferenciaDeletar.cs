using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ReferenciaDeletar
    {
        public Aeronave Aeronave { get; set; }
        public Usuario Usuario { get; set; }
        public Aeroporto Aeroporto { get; set; }
        public Passageiro Passageiro { get; set; }
        public PrecoBase PrecoBase { get; set; }
        public PassagemAerea PassagemAerea { get; set; }
        public Voo Voo { get; set; }
    }
}
