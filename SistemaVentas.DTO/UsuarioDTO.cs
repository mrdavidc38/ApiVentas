﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Correo { get; set; }

        public string? RolDescription { get; set; }

        public string? Clave { get; set; }

        public int? EsActivo { get; set; }
    }
}
