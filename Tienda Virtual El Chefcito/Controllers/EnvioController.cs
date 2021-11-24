using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Almacenista.Models;

namespace Almacenista.Controllers
{
    public class EnvioController : Controller
    {
        private contextAlmacen db = new contextAlmacen();

        // GET: Envio
        public ActionResult Index()
        {
            var envios = db.Envios.Where(e => e.fecha_envio == null).Include(e => e.Paqueterias);
            return View(envios.ToList());
        }
        public ActionResult Index1()
        {
            var envios = db.Envios.Where(e => e.fecha_entrega == null && e.fecha_envio!=null).Include(e => e.Paqueterias);
            return View(envios.ToList());
        }
        public ActionResult Index2()
        {
            var envios = db.Envios.Where(e => e.fecha_entrega != null && e.fecha_envio != null).Include(e => e.Paqueterias);
            return View(envios.ToList());
        }
        // GET: Envio/Details/5
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
        // GET: Envio/Details/5
        public ActionResult Details1(int? id)
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
        // GET: Envio/Details/5
        public ActionResult Details2(int? id)
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

        // GET: Envio/Create
        public ActionResult Create()
        {
            ViewBag.id_detalle_venta = new SelectList(db.Detalle_venta, "id_detalle_venta", "id_detalle_venta");
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre");
            return View();
        }

        // POST: Envio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_envio,contacto,numero_guia,direccion,estatus,fecha_envio,fecha_entrega,id_paqueteria,id_detalle_venta")] Envios envios)
        {
            if (ModelState.IsValid)
            {
                db.Envios.Add(envios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_detalle_venta = new SelectList(db.Detalle_venta, "id_detalle_venta", "id_detalle_venta", envios.id_detalle_venta);
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre", envios.id_paqueteria);
            return View(envios);
        }

        // GET: Envio/Edit/5
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
            ViewBag.id_detalle_venta = new SelectList(db.Detalle_venta, "id_detalle_venta", "id_detalle_venta", envios.id_detalle_venta);
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre", envios.id_paqueteria);
            return View(envios);
        }

        // GET: Envio/Edit/5
        public ActionResult Edit1(int? id)
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
            ViewBag.id_detalle_venta = new SelectList(db.Detalle_venta, "id_detalle_venta", "id_detalle_venta", envios.id_detalle_venta);
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre", envios.id_paqueteria);
            return View(envios);
        }

        // POST: Envio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_envio,numero_guia,estatus,fecha_envio,id_paqueteria")] Envios envios)
        {
            if (ModelState.IsValid)
            {
                Envios o = db.Envios.Find(envios.id_envio);
                o.numero_guia = envios.numero_guia;
                o.estatus = envios.estatus;
                o.fecha_envio = envios.fecha_envio;
                o.id_paqueteria = envios.id_paqueteria;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_detalle_venta = new SelectList(db.Detalle_venta, "id_detalle_venta", "id_detalle_venta", envios.id_detalle_venta);
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre", envios.id_paqueteria);
            return View(envios);
        }
        // POST: Envio/Edit1/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "id_envio,estatus,estatus,fecha_entrega,id_paqueteria")] Envios envios)
        {
            if (ModelState.IsValid)
            {
                Envios o = db.Envios.Find(envios.id_envio);
                o.estatus = envios.estatus;
                o.fecha_entrega = envios.fecha_entrega;
                db.SaveChanges();
                return RedirectToAction("Index1");
            }
            ViewBag.id_detalle_venta = new SelectList(db.Detalle_venta, "id_detalle_venta", "id_detalle_venta", envios.id_detalle_venta);
            ViewBag.id_paqueteria = new SelectList(db.Paqueterias, "id_paqueteria", "nombre", envios.id_paqueteria);
            return View(envios);
        }

        // GET: Envio/Delete/5
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

        // POST: Envio/Delete/5
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
