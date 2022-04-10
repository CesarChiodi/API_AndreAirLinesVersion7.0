using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IngestaoDadosAeroporto.Modelo;
using IngestaoDadosAeroporto.Repositorio;

namespace IngestaoDadosAeroporto.Servico
{
    public class ServicoAeroportoSQL
    {
        private IAeroportoRepository _aeroportoRepository;

        public ServicoAeroportoSQL()
        {
            _aeroportoRepository = new AeroportoRepository();
        }

        public bool Add(Aeroporto aeroporto)
        {
            return _aeroportoRepository.Add(aeroporto);
        }
        public List<Aeroporto> GetAll()
        {
            return _aeroportoRepository.GetAll();
        }

        public static List<Aeroporto> LerCsv()
        {
            var lista = new List<Aeroporto>();
            string path = @"C:\Users\Cesar C Siqueira\source\repos\API_AndreAirLinesVersion3\IngestaoDadosAeroporto\File\Dados.csv";
            using (var leitor = new StreamReader(path))
            {
                var linha = leitor.ReadLine();
                while (linha != null)
                {
                    linha = leitor.ReadLine();
                    if (linha != null)
                    {
                        var values = linha.Split(';');
                        lista.Add(new Aeroporto(values[0], values[1], values[2], values[3]));
                    }
                }
            }
            return lista;
        }
    }
}
