using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreAirLinesFrontAPI.Models;

namespace AndreAirLinesFrontAPI.Data
{
    public class AndreAirLinesFrontAPIContext : DbContext
    {
        public AndreAirLinesFrontAPIContext (DbContextOptions<AndreAirLinesFrontAPIContext> options)
            : base(options)
        {
        }

        public DbSet<AndreAirLinesFrontAPI.Models.AeroportoDadosDapper> AeroportoDadosDapper { get; set; }

      //  public DbSet<AndreAirLinesFrontAPI.Models.AeroportoMicroServico> AeroportoMicroServico { get; set; }
    }
}
