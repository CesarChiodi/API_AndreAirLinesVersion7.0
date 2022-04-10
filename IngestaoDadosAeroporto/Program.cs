using System;
using IngestaoDadosAeroporto.Modelo;
using IngestaoDadosAeroporto.Configuracao;
using IngestaoDadosAeroporto.Servico;
using System.Collections.Generic;

namespace IngestaoDadosAeroporto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*ServicoAeroportoSQL servico = new();
            List<Aeroporto> dadosAeroporto = new();
            dadosAeroporto = ServicoAeroportoSQL.LerCsv();
            dadosAeroporto.ForEach(aeroporto =>
            {
                servico.Add(aeroporto);

            });*/

            foreach (var arg in new ServicoAeroportoSQL().GetAll())
            {
                Console.WriteLine(arg);
            } 

        }
    }
}
