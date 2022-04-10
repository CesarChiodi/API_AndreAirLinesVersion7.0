using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaDapperSQL.Modelo
{
    public class AeroportoCD
    {
        public readonly static string INSERT = "INSERT INTO Aeroporto (City, Country, Code, Continent) VALUES (@City, @Country, @Code, @Continent)";
        public readonly static string GETALL = "SELECT Id, City, Country, Code, Continent FROM Aeroporto";
        public readonly static string GETID = "SELECT Id, City, Country, Code, Continent FROM Aeroporto WHERE Id = @Id";

        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Continent { get; set; }

        public AeroportoCD()
        {
        }

        public AeroportoCD(string city, string country, string code, string continent)
        {
            City = city;
            Country = country;
            Code = code;
            Continent = continent;
        }

        public override string ToString()
        {
            return "Id: " + Id + "City: " + City + "Country: " + Country + "Code: " + Code + "Continent: " + Continent;
        }
    }
}
