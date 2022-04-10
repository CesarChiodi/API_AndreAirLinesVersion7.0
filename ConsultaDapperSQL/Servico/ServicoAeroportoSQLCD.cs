using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaDapperSQL.Modelo;
using ConsultaDapperSQL.Repositorio;

namespace ConsultaDapperSQL.Servico
{
    public class ServicoAeroportoSQLCD
    {
        private IAeroportoRepositoryCD _aeroportoRepository;

        public ServicoAeroportoSQLCD()
        {
            _aeroportoRepository = new AeroportoRepositoryCD();
        }

        public bool Add(AeroportoCD aeroporto)
        {
            return _aeroportoRepository.Add(aeroporto);
        }
        public List<AeroportoCD> GetAll()
        {
            return _aeroportoRepository.GetAll();
        }
        public AeroportoCD GetId(int id)
        {
            return _aeroportoRepository.GetId(id);
        }
    }
}
