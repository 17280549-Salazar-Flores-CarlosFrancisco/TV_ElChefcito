using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda_Virtual_El_Chefcito.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Comentario agregado por Carlos desde la Rama "main"
            //Comentario 2 agregado por Carlos desde la Rama "main   [Commit 2]"

            //[Commit 3] Cambio hecho por Carlos desde la Rama "main (Ya solo se actualiza el código)"
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    //C
}