﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.DTO
{
    public class DetalleVentaDTO
    {
        public int? IdProducto { get; set; }
        public string? DescriptionProducto { get; set; }

        public int? Cantidad { get; set; }

        public string? PrecioTexto { get; set; }

        public string? TotalTexto { get; set; }
    }
}
