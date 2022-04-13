using System;

namespace Robo.Servico
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            //Consulta consulta = new Consulta();
            int opc;
            int mes;
            do
            {
                Console.WriteLine("\t____________________________________________");
                Console.WriteLine("\t|+++++++++++++++++| MENU |+++++++++++++++++|");
                Console.WriteLine("\t|1| - INSERIR MASSA DE DADOS               |");
                Console.WriteLine("\t|2| - BUSCAR REGISTRO DE PASSAGEM AEREA    |");
                Console.WriteLine("\t|3| - BUSCAR REGISTRO DE PRECO BASE        |");
                Console.WriteLine("\t|4| - COMPARAR DAPPER X ENTITY FRAMEWORK   |");
                Console.WriteLine("\t|0| - SAIR                                 |");
                Console.Write("\t|__________________________________________|\n" +
                              "\t|Opção: ");
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Inserir.Insertion().Wait();
                        break;
                    case 2:
                        Console.WriteLine("\tInforme o Mes para Consulta\n");
                        Console.WriteLine("\tUtilize o Padrao Numerico de Datas: --/MM/---- ");
                        mes = int.Parse(Console.ReadLine());
                        if (mes <= 0)
                        {
                            Console.WriteLine("Informe um Mes Valido para Consulta");
                        }
                        else
                        {
                            Consulta.RelatorioPassagemAerea(mes).Wait();
                        }
                        break;
                    case 3:
                        Consulta.RelatorioPrecoBase().Wait();
                        break;
                    case 4:
                        Dapper_X_EntityFramework compara = new();
                        compara.Comparacao();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção invalida!");
                        break;
                }
                Console.ReadKey();
                Console.Clear();

            } while (opc != 0);
        }
    }
}
