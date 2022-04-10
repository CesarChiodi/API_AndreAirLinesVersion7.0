using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaDapperSQL.Modelo;

namespace ConsultaDapperSQL.Repositorio
{
    public interface IAeroportoRepositoryCD
    {
        bool Add(AeroportoCD aeroporto);
        List<AeroportoCD> GetAll();
        AeroportoCD GetId(int id);
    }
}
