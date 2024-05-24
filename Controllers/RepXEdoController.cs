using Microsoft.AspNetCore.Mvc;
using NETCORE_ALUM.Models;

namespace NETCORE_ALUM.Controllers
{
    public class RepXEdoController : Controller
    {
        public IActionResult Index()
        {
            RepXEdo oRep = new RepXEdo();
            UtlRepXEdo oUtlrep = new UtlRepXEdo();
            oRep.Link = oUtlrep.FNRuta();
            return View(oRep);
        }
    }
}
