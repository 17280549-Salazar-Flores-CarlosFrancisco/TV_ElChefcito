//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tienda_Virtual_El_Chefcito.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class orden_producto
    {
        public int id_orden { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
    
        public virtual orden orden { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
