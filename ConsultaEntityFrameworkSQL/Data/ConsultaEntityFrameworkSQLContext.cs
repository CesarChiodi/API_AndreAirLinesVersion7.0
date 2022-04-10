using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IngestaoDadosAeroporto.Modelo;

namespace ConsultaEntityFrameworkSQL.Data
{
    public class ConsultaEntityFrameworkSQLContext : DbContext
    {
        public ConsultaEntityFrameworkSQLContext (DbContextOptions<ConsultaEntityFrameworkSQLContext> options)
            : base(options)
        {
        }

        public DbSet<IngestaoDadosAeroporto.Modelo.Aeroporto> Aeroporto { get; set; }
    }
}
