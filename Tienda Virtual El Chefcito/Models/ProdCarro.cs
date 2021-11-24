using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tienda_Virtual_El_Chefcito.Models;

namespace Tienda_Virtual_El_Chefcito.Models
{
    public class ProdCarro
    {
        private contextTienda db = new contextTienda();
        private List<Productos> products;
        public ProdCarro()
        {
            products = db.Productos.ToList();
        }
        public List<Productos> findAll()
        {
            return this.products;
        }
        public Productos find(int id)
        {
            Productos pp = this.products.Single(p => p.id_producto.Equals(id));
            return pp;
        }
    }
}