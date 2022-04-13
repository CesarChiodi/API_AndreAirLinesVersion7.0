using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Funcao
    {
        public int IdFuncao { get; set; }
        public string DescricaoFuncao { get; set; }
        List<Acesso> ListaAcessos { get; set;}
    }
}
