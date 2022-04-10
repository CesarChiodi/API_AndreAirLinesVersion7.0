using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IngestaoDadosAeroporto.Modelo;

namespace IngestaoDadosAeroporto.Repositorio
{
    public interface IAeroportoRepository
    {
            bool Add(Aeroporto aeroporto);
            List<Aeroporto> GetAll();
    }
}
