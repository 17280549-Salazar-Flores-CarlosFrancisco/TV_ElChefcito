using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda_Virtual_El_Chefcito.Models;

namespace Tienda_Virtual_El_Chefcito.Controllers
{
    public class CatalogoController : Controller
    {
        private contextTienda db = new contextTienda();
        // GET: Catalogo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarProd(string nomBuscar)
        {
            ViewBag.SearchKey = nomBuscar;
            using (db)
            {
                var query = from st in db.Productos
                            where st.nombre.Contains(nomBuscar)

                            select st;
                var listProd = query.ToList();
                ViewBag.Listado = listProd;
            }
            return View();
        }

        public ActionResult DetalleProd(int id)
        {

            ViewBag.SearchKey = id;
            using (db)
            {
                var query = from st in db.Productos
                            where st.id_producto.Equals(id)

                            select st;
                var listProd = query.ToList();
                ViewBag.Listado = listProd;
            }
            return View();
        }

        public ActionResult productos()
        {
            List<Productos> mercancia = null;
            var query = from p in db.Productos
                        select p;
                List<Productos> productos = query.ToList();
                mercancia = query.ToList();
            ViewBag.productos = mercancia;
            return View();
        }

        public ActionResult categorias()
        {
            List<Categorias> mercancia = null;
            var query = from p in db.Categorias
                        select p;
            List<Categorias> categorias = query.ToList();
            mercancia = query.ToList();
            ViewBag.Catego = mercancia;
            return View();
        }

        public ActionResult subCategorias()
        {
            List<Subcategorias> mercancia = null;
            var query = from p in db.Subcategorias
                        select p;
            List<Subcategorias> subcategorias = query.ToList();
            mercancia = query.ToList();
            ViewBag.SubCatego = mercancia;
            return View();
        }

        public ActionResult prodCategoria(int idCat)
        {
            List<Productos> mercancia = null;
            var query = from p in db.Productos
                        from s in db.Subcategorias
                        where s.id_categoria == idCat
                        where s.id_categoria == p.id_subcategoria
                        select p;

            if (idCat == 1)
            {
                List<Productos> lista = query.ToList();
                mercancia = query.ToList();
                ViewBag.Catego = "Equipos de cocción";
            }

            if (idCat == 5)
            {
                List<Productos> lista = query.ToList();
                mercancia = lista;
                ViewBag.Catego = "Equipos de refrigeración";
            }

            if (idCat == 6)
            {
                List<Productos> lista = query.ToList();
                mercancia = lista;
                ViewBag.Catego = "Lavaloza";
            }
            ViewBag.productos = mercancia;
            return View();
        }

        public ActionResult prodSubCategoria(int idCat)
        {
            List<Productos> mercancia = null;
            var query = from p in db.Productos
                        where p.id_subcategoria == idCat
                        select p;

            if (idCat == 2)
            {
                List<Productos> lista = query.ToList();
                mercancia = query.ToList();
                ViewBag.Sub = "Asadores";
            }

            if (idCat == 3)
            {
                List<Productos> lista = query.ToList();
                mercancia = lista;
                ViewBag.Sub = "Freidoras";
            }

            if (idCat == 4)
            {
                List<Productos> lista = query.ToList();
                mercancia = lista;
                ViewBag.Sub = "Refrigeradores";
            }

            if (idCat == 5)
            {
                List<Productos> lista = query.ToList();
                mercancia = lista;
                ViewBag.Sub = "Fábricas de Hielo";
            }

            if (idCat == 6)
            {
                List<Productos> lista = query.ToList();
                mercancia = lista;
                ViewBag.Sub = "Platos";
            }

            if (idCat == 7)
            {
                List<Productos> lista = query.ToList();
                mercancia = lista;
                ViewBag.Sub = "Cubiertos";
            }

            ViewBag.productos = mercancia;
            return View();
        }



    }
}