using Microsoft.AspNetCore.Mvc;
using Parcial_MilagrosPeyretti.Datos;
using Parcial_MilagrosPeyretti.Models;

namespace Parcial_MilagrosPeyretti.Controllers
{
    public class CompetidoresController : Controller
    {
        DatosCompetidores _BD = new DatosCompetidores();
        public IActionResult Index()
        {
            return View(_BD.ListaCompetidores());
        }

        public IActionResult Create()
        {
            ViewBag.ListaDisciplinas = _BD.ListaDisciplinas();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Competidor competidor)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View();
                }
                ViewBag.Error = _BD.RegistrarCompetidores(competidor);
                if (ViewBag.Error != "")
                {
                    return View("");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public IActionResult ListarDisciplinas()
        {
            return View(_BD.ListarCantidadDisciplina());
        }
    }
}
