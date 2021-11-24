﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Actividad3Vistas.Models;

namespace Actividad3Vistas.Controllers
{
    [Authorize]
    public class PedidoPendienteController : Controller
    {

        private contextRestaurantes db = new contextRestaurantes();       
        // GET: PedidoPendiente
        public ActionResult Index(int? idU)
        {
            List<Ventas> uventas = db.Ventas.ToList();
            List<Detalle_venta> udetven = db.Detalle_venta.ToList();
            List<Envios> uenv = db.Envios.ToList();
            List<Productos> uprod = db.Productos.ToList();
            var query = from v in uventas
                        join p in uprod on v.id_producto equals p.id_producto into table1
                        from p in table1.DefaultIfEmpty()
                        join dv in udetven on v.id_detalle_venta equals dv.id_detalle_venta into table2
                        from dv in table2.DefaultIfEmpty()
                        join e in uenv on dv.id_detalle_venta equals e.id_detalle_venta into table3
                        from e in table3.DefaultIfEmpty()
                        where v.id_usuario == idU && e.estatus != 1
                        select new ListaPedidoPendiente { producto = p, ventas = v, det_ven = dv, envio = e };
            ViewBag.pedidospen = query;
            return View();
        }

        public ActionResult Details(int idP, string cant, string fechE, string total)
        {
            var query= (from p in db.Productos
                       where p.id_producto == idP
                       select p).FirstOrDefault();
            ViewBag.detprod = query;
            ViewBag.cant = cant;
            ViewBag.fechE = fechE;
            ViewBag.total = total;
            return View();
        }
    }
}