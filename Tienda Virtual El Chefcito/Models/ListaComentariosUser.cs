using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Actividad3Vistas.Models
{
    public class ListaComentariosUser
    {
        public Usuarios usuarios { get; set; }
        public Ventas ventas { get; set; }
        public Comentario comentario { get; set; }
    }
}