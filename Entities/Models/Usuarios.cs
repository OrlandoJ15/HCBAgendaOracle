using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Models
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdRol { get; set; }
        public string Correo { get; set; } = null!;
        public string? Clave { get; set; }
        public byte Estado { get; set; }

        [NotMapped]
        public string? NombreRol { get; set; } = null!;

        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}
