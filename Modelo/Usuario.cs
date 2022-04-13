using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Usuario : Pessoa
    {
        public string Senha { get; set; }
        public string Login { get; set; }
        public string Setor { get; set; }
        public Funcao Funcao { get; set; }
    }
}
