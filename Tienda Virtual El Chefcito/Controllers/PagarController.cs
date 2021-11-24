using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Actividad3Vistas.Models;
namespace Actividad3Vistas.Controllers
{
    [Authorize]
    public class PagarController : Controller
    {
        private contextRestaurantes db = new contextRestaurantes();
        [AllowAnonymous]
        // GET: Pagar
        public ActionResult Pagar()
        {
            if(!User.Identity.IsAuthenticated)
            {
                Session["CreateOrder"] = "Pendiente";
                return RedirectToAction("Login", "Account");
            }
            int idc = Convert.ToInt32(Session["idclien"].ToString());
            var query = (from u in db.Usuarios
                        where u.id_usuario == idc
                         select u
                        ).FirstOrDefault();
            int cMonth = Convert.ToInt32(DateTime.Now.ToString("MM"));
            int cYear = Convert.ToInt32(DateTime.Now.ToString("yy"));
            ViewBag.anio = cYear;
            ViewBag.mes = cMonth;
            ViewBag.emailcliente = query.email;
            ViewBag.nombrecliente = query.nombre+" "+query.apellidos;
            return View();
        }
        public ActionResult CreateOrder(string titular, string digitos, string fechaexp, string cvv, string email)
        {
            string tipotarj = "";
            if(checkCard(digitos,fechaexp))
            {
                if(checkCardType(digitos,tipotarj))
                {
                    if(IsCardValid(cvv))
                    {
                        return RedirectToAction("Create","Envios");
                    }
                }
            }

            return RedirectToAction("CardDeclined");
        }
        public ActionResult PagoPayPal()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["CreateOrder"] = "Pendiente";
                return RedirectToAction("Login", "Account");
            }
            int idc = Convert.ToInt32(Session["idclien"].ToString());
            var query = (from u in db.Usuarios
                         where u.id_usuario == idc
                         select u
                        ).FirstOrDefault();
            ViewBag.emailcliente = query.email;
            ViewBag.nombrecliente = query.nombre + " " + query.apellidos;
            return View();
        }
        public ActionResult VerifyPayPal(string pwd)
        {
            if(pwd != null || pwd == "")
            {
                return RedirectToAction("Create", "Envios");
            }
            return RedirectToAction("CardDeclined");
        }
        public ActionResult pagoProcesado()
        {
            return View();
        }
        public ActionResult pagoNoProcesado()
        {
            return View();
        }
        public ActionResult CardDeclined()
        {
            return View();
        }
        private bool checkCard(string digitos, string fechaexp)
        {
            bool passed = false;
            int mes_tarj = Convert.ToInt32(fechaexp.Substring(0,2));
            int anio_tarj = Convert.ToInt32(fechaexp.Substring(3, 2));
            int cMonth = Convert.ToInt32(DateTime.Now.ToString("MM"));
            int cYear = Convert.ToInt32(DateTime.Now.ToString("yy"));
            if (digitos.Length==16)
            {
                 if (anio_tarj > cYear)
                 {
                     passed = true;
                 }else if(cYear == anio_tarj)
                 {
                     if(mes_tarj > cMonth)
                     {
                         passed = true;
                     }
                 }
            }
            return passed;
        }
        private bool checkCardType(string digitos, string tipoTarj)
        {
            bool passed = false;
            if(digitos.StartsWith("4"))
            {
                tipoTarj = "Mastercard";
                passed = true;
            }else if(digitos.StartsWith("5"))
            {
                tipoTarj = "Visa";
                passed = true;
            }else if (digitos.StartsWith("3"))
            {
                tipoTarj = "American Express";
                passed = true;
            }
            else
            {
                tipoTarj = "Tarjeta no reconocida";
            }
            return passed;
        }
        private bool IsCardValid(string cvv)
        {
            bool valid = false;
            if(cvv.Length == 3)
            {
                valid = true;
            }
            return valid;
        }
    }
}