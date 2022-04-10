using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConsultaEntityFrameworkSQL.Data;
using IngestaoDadosAeroporto.Modelo;

namespace ConsultaEntityFrameworkSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeroportoesController : ControllerBase
    {
        private readonly ConsultaEntityFrameworkSQLContext _context;

        public AeroportoesController(ConsultaEntityFrameworkSQLContext context)
        {
            _context = context;
        }

        // GET: api/Aeroportoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aeroporto>> GetAeroporto(int id)
        {
            var aeroporto = await _context.Aeroporto.FindAsync(id); //Service

            if (aeroporto == null)
            {
                return NotFound();
            }

            return aeroporto;
        }
    }
}
