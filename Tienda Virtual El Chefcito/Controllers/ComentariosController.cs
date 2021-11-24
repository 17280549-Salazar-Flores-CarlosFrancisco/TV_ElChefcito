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
    [Authorize]
    public class ComentariosController : Controller
    {
        private contextRestaurantes db = new contextRestaurantes();

        // GET: Comentarios
        public ActionResult Index(int? idU)
        {
            List<Usuarios> users = db.Usuarios.ToList();
            List<Ventas> sales = db.Ventas.ToList();
            List<Comentario> comments = db.Comentario.ToList();

            var query = from c in comments
                        join s in sales on c.id_venta equals s.id_venta into table1
                        from s in table1.DefaultIfEmpty()
                        join u in users on s.id_usuario equals u.id_usuario into table2
                        from u in table2.DefaultIfEmpty()
                        where u.id_usuario == idU
                        select new ListaComentariosUser { usuarios = u, ventas = s, comentario = c };
            ViewBag.miscomentarios = query;
            return View();
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View();
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.comentario = comentario;
            return View();
        }

        // GET: Comentarios/Create
        public ActionResult Create( int idVent)
        {
            ViewBag.id_venta = idVent;
            return View();
        }

        // POST: Comentarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_comentario,titulo,descripcion,fecha,respuesta,estatus,id_venta")] Comentario comentario)
        public ActionResult Create(string titulo, string descripcion, int id_venta) 
        {
            if(titulo == null || descripcion == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Comentario comentario = new Comentario();
                int idComment = -1;
                if (!(db.Comentario.Max(c => (int?) c.id_comentario) == null))
                {
                    idComment = db.Comentario.Max(c => c.id_comentario);
                }
                else
                {
                    idComment = 0;
                }
                idComment++;
                comentario.id_comentario = idComment;
                comentario.fecha = DateTime.Today;
                comentario.titulo = titulo;
                comentario.descripcion = descripcion;
                comentario.estatus = 0;
                comentario.id_venta = id_venta;
                db.Comentario.Add(comentario);
                db.SaveChanges();
                int idUse = Convert.ToInt32(Session["idclien"].ToString());
                return RedirectToAction("Index", new { idU= idUse});
            }
            
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_venta = new SelectList(db.Ventas, "id_venta", "id_venta", comentario.id_venta);
            return View(comentario);
        }
        // GET: Comentarios/Edit/5
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_venta = new SelectList(db.Ventas, "id_venta", "id_venta", comentario.id_venta);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_comentario,titulo,descripcion,fecha,respuesta,estatus,id_venta")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_venta = new SelectList(db.Ventas, "id_venta", "id_venta", comentario.id_venta);
            return View(comentario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2(int id_comentario, string titulo, string descripcion)
        {
            Comentario comentario = db.Comentario.Find(id_comentario);
            if(titulo != null && descripcion != null)
            {
                comentario.titulo = titulo;
                comentario.descripcion = descripcion;
                comentario.estatus = 0;
                comentario.fecha = DateTime.Today;
                db.SaveChanges();
            }
            /*
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_venta = new SelectList(db.Ventas, "id_venta", "id_venta", comentario.id_venta);*/
            int idClien = Convert.ToInt32(Session["idclien"].ToString());
            return RedirectToAction("Index", new { idU = idClien });
        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentario.Find(id);
            db.Comentario.Remove(comentario);
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
