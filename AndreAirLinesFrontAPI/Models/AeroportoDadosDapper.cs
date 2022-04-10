using System.ComponentModel.DataAnnotations.Schema;

namespace AndreAirLinesFrontAPI.Models
{
    [Table("Aeroporto")]
    public class AeroportoDadosDapper
    {

        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Continent { get; set; }
    }
}
