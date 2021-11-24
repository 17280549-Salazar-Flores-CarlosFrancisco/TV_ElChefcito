using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Actividad3Vistas.Models;

namespace Actividad3Vistas.Controllers
{
    public class EnviosController : Controller
    {
        private contextRestaurantes db = new contextRestaurantes();

        // GET: Envios
        public ActionResult Index()
        {
            var envios = db.Envios.Include(e => e.Paqueterias);
            return View(envios.ToList());
        }

        // GET: Envios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Envios envios = db.Envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            return View(envios);
        }

        // GET: Envios/Create
        public ActionResult Create()
        {
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre");
            return View();
        }

        // POST: Envios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // public ActionResult Create([Bind(Include = "id_envios,contacto,numero_guia,direccion,fecha_envio,fecha_entrega,id_venta,id_paqueteria")] Envios envios)
        public ActionResult Create(string contacto, string calle, string estado, string municipio, string codpost)
        {
            
            if(ValidateInfo(contacto, calle, estado, municipio, codpost))
            {
                Detalle_venta detventa = new Detalle_venta();
                int id = 0;
                if (!(db.Detalle_venta.Max(v => (int?)v.id_detalle_venta) == null))
                {
                    id = db.Detalle_venta.Max(v => v.id_detalle_venta);
                }
                else
                {
                    id = 0;
                }
                id++;
                var carro = Session["cart"] as List<Item>;
                detventa.id_detalle_venta = id;
                detventa.subtotal = carro.Sum(item => item.Producto.precio_venta * item.Cantidad);
                detventa.iva = 0;
                detventa.total = detventa.subtotal + 200;
                detventa.estatus = 1;
                detventa.costo_envio = 200;
                db.Detalle_venta.Add(detventa);
                db.SaveChanges();
                Ventas venta;
                int id2;
                foreach(Item prod in carro)
                {
                    venta = new Ventas();
                    if (!(db.Ventas.Max(v => (int?)v.id_venta) == null))
                    {
                        id2 = db.Ventas.Max(v => v.id_venta);
                    }
                    else
                    {
                        id2 = 0;
                    }
                    id2++;
                    venta.id_venta = id2;
                    venta.fecha_venta = DateTime.Today;
                    venta.cantidad = prod.Cantidad;
                    venta.precio_venta = prod.Producto.precio_venta;
                    venta.estatus = 1;
                    venta.id_producto = prod.Producto.id_producto;
                    venta.id_usuario = Convert.ToInt32(Session["idclien"].ToString());
                    venta.id_detalle_venta = detventa.id_detalle_venta;
                    db.Ventas.Add(venta);
                    db.SaveChanges();
                }
                Envios envio = new Envios();
                int id3 = 0;
                if (!(db.Envios.Max(e => (int?)e.id_envio) == null))
                {
                    id3 = db.Envios.Max(e => e.id_envio);
                }
                else
                {
                    id3 = 0;
                }
                id3++;
                envio.id_envio = id3;
                envio.contacto = contacto;
                envio.direccion = calle + ", " + codpost + ", " + municipio + ", " + estado;
                envio.id_detalle_venta = detventa.id_detalle_venta;
                db.Envios.Add(envio);
                db.SaveChanges();
                Session["cart"] = null;
                Session["itemTotal"] = 0;
                return RedirectToAction("pagoProcesado", "Pagar");
            }
            return RedirectToAction("pagoNoProcesado", "Pagar");
        }

        private bool ValidateInfo(string contacto, string calle, string estado, string municipio, string codpost)
        {
            bool isOk = true;
            if(contacto.Equals("") || calle.Equals("") || estado.Equals("") || municipio.Equals("") || codpost.Equals(""))
            {
                isOk = false;
            }
            return isOk;
        }
        // GET: Envios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Envios envios = db.Envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre", envios.id_paqueteria);
            return View(envios);
        }

        // POST: Envios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_envio,contacto,numero_guia,direccion,estatus,fecha_envio,fecha_entrega,id_detalle_venta,id_paqueteria")] Envios envios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(envios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre", envios.id_paqueteria);
            return View(envios);
        }

        // GET: Envios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Envios envios = db.Envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            return View(envios);
        }

        // POST: Envios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Envios envios = db.Envios.Find(id);
            db.Envios.Remove(envios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
