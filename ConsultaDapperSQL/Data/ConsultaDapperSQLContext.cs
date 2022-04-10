using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsultaDapperSQL.Modelo;

namespace ConsultaDapperSQL.Data
{
    public class ConsultaDapperSQLContext : DbContext
    {
        public ConsultaDapperSQLContext (DbContextOptions<ConsultaDapperSQLContext> options)
            : base(options)
        {
        }

        public DbSet<ConsultaDapperSQL.Modelo.AeroportoCD> AeroportoCD { get; set; }
    }
}
