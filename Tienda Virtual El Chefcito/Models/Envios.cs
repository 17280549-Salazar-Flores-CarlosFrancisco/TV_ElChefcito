//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Actividad3Vistas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Envios
    {
        public int id_envio { get; set; }
        public string contacto { get; set; }
        public string numero_guia { get; set; }
        public string direccion { get; set; }
        public Nullable<int> estatus { get; set; }
        public Nullable<System.DateTime> fecha_envio { get; set; }
        public Nullable<System.DateTime> fecha_entrega { get; set; }
        public int id_detalle_venta { get; set; }
        public Nullable<int> id_paqueteria { get; set; }
    
        public virtual Detalle_venta Detalle_venta { get; set; }
        public virtual Paqueterias Paqueterias { get; set; }
    }
}
