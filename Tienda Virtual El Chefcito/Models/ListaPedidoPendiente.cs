using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Actividad3Vistas.Models
{
    public class ListaPedidoPendiente
    {
        public Productos producto { get; set; }
        public Ventas ventas { get; set; }
        public Detalle_venta det_ven { get; set; }
        public Envios envio { get; set; }

      
    }
}