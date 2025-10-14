using System;
using System.Collections.Generic;

namespace Entidades.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
