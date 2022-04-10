using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConsultaDapperSQL.Data;
using ConsultaDapperSQL.Modelo;
using ConsultaDapperSQL.Servico;

namespace ConsultaDapperSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeroportoCDsController : ControllerBase
    {
        private readonly ConsultaDapperSQLContext _context;

        public AeroportoCDsController(ConsultaDapperSQLContext context)
        {
            _context = context;
        }
        
        // GET: api/AeroportoCDs/5

        [HttpGet("{id}")]
        public ActionResult<AeroportoCD> GetAeroportoCD(int id)
        {
            var aeroportoCD = new ServicoAeroportoSQLCD().GetId(id);

            if (aeroportoCD == null)
            {
                return NotFound();
            }

            return aeroportoCD;
        }
    }
}
