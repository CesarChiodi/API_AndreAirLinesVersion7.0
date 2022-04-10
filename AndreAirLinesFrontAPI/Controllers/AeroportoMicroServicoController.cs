using AndreAirLinesFrontAPI.Servico;
using Microsoft.AspNetCore.Mvc;

namespace AndreAirLinesFrontAPI.Controllers
{
    public class AeroportoMicroServicoController : Controller
    {
        private readonly ServicoAeroportoFront _servico;

        public AeroportoMicroServicoController(ServicoAeroportoFront context)
        {
            _servico = context;
        }

        // GET: AeroportoMicroServicoes
        public IActionResult Index()
        {
            return View( _servico.Get());
        }
    }
}
