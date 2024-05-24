using Microsoft.AspNetCore.Mvc;
using NETCORE_ALUM.Models;

namespace NETCORE_ALUM.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            Alumno oAlumno = new Alumno();
            Inicio oIni = new Inicio();
            oAlumno.WNom = " PPP " + DateTime.Now.ToString();
            oAlumno.IEnumAlumnos = oIni.GetAlumnos();
            return View(oAlumno);
        }
    }
}
