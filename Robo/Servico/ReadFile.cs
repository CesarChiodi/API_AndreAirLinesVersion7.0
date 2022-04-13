using System.Collections.Generic;
using System.IO;
using Modelo;
using Newtonsoft.Json;

namespace Robo.Servico
{
    public class ReadFile
    {
        public static List<Root> GetData(string pathFileVoo)
        {
            StreamReader reader = new StreamReader(pathFileVoo);
            string jsonString = reader.ReadToEnd();
            var lista = JsonConvert.DeserializeObject<List<Root>>(jsonString) as List<Root>;
            if (lista != null)
                return lista;
            return null;
        }
    }
}
