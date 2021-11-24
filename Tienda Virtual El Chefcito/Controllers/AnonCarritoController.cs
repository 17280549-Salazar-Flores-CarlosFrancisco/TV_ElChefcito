using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda_Virtual_El_Chefcito.Models;

namespace Tienda_Virtual_El_Chefcito.Controllers
{
    public class AnonCarritoController : Controller
    {
        // GET: AnonCarrito
        public ActionResult Shopped()
        {
            return View();
        }

        public ActionResult Agregar(int id)
        {

            ProdCarro carro = new ProdCarro();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                Productos P = carro.find(id);
                string nam = P.nombre;
                cart.Add(new Item { Producto = carro.find(id), Cantidad = 1 });
                Session["cart"] = cart;
                Session["itemTotal"] = 1;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Cantidad++;
                }
                else
                {
                    Productos P = carro.find(id);
                    string nam = P.nombre;
                    cart.Add(new Item { Producto = carro.find(id), Cantidad = 1 });
                    Session["itemTotal"] = (Int32.Parse(Session["itemTotal"].ToString())) + 1;
                }
                Session["cart"] = cart;

            }
            return RedirectToAction("Shopped");
        }
        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Producto.id_producto.Equals(id))
                    return i;
            }
            return -1;
        }
        public ActionResult Quitar(int id)
        {
            Session["itemTotal"] = (Int32.Parse(Session["itemTotal"].ToString())) - 1;
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Shopped");
        }
    }
}
