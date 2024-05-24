using Microsoft.AspNetCore.Mvc;
using NETCORE_ALUM.Models;

namespace NETCORE_ALUM.Controllers
{
    public class AlumnoController : Controller
    {
        public IActionResult Index()
        {
            Alumno alumno = new Alumno();
            return View(alumno.FNConsAlumno(0));
        }
        [HttpPost]
        public ActionResult Guardar(IFormCollection oForm)
        {
            string WNombre = oForm["txtNombre"].ToString();
            string WApe = oForm["txtApe"].ToString();
            string nIDAlum = oForm["HDIDAlum"].ToString();
            string IDEst = oForm["IDEst"].ToString();
            Alumno oAlumno = new Alumno();
            oAlumno.SPGuardaAlumno(WNombre, WApe, nIDAlum, IDEst);
            oAlumno = null;
            return RedirectToAction("Index", "Inicio");
        }
        public ActionResult Actualizar(int IDAlum)
        {
            Alumno alumno = new Alumno();
            return View("Index", alumno.FNConsAlumno(IDAlum));
        }
        public ActionResult Borrar(string IDAlum)
        {
            Alumno oAlumno = new Alumno();
            oAlumno.SPBorrarAlumno(IDAlum);
            oAlumno = null;
            return RedirectToAction("Index", "Inicio");
        }
    }
}
