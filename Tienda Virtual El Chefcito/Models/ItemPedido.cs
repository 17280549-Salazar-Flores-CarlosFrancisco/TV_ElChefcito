using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tienda_Virtual_El_Chefcito.Models;

namespace Tienda_Virtual_El_Chefcito.Models
{
    public class ItemPedido
    {
        public int idOrd
        {
            get;
            set;
        }
        public Productos Produc
        {
            get;
            set;
        }
        public int Cantidad
        {
            get;
            set;
        }
    }
}