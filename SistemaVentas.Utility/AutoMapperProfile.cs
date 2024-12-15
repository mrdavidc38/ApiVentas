﻿using AutoMapper;
using SistemaVentas.DTO;
using SIstemaVentas.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>().ForMember(destino =>
            destino.RolDescription,
            opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
            ).ForMember(destino => destino.EsActivo,
            opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));
          

          
            CreateMap<Usuario, SesionDTO>().ForMember(destino => destino.RolDescription,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre));
           
            CreateMap<UsuarioDTO, Usuario>().ForMember(destino => destino.IdRolNavigation,
                opt => opt.Ignore()).ForMember(destino => destino.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion  

            #region Producto
            CreateMap<Producto, ProductoDTO>().ForMember(destino => destino.DescriptionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destino => destino.Precio, opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO"))
                )).ForMember(destino => destino.EsActivo,
            opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));
            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>().ForMember(
               destino => destino.TotalTexto,
               opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))))
                .ForMember(destino=>destino.FechaRegistro, opt=>opt.MapFrom(origen=>origen.FechaRegistro.Value.ToString("dd/MM/yyyy")));

            CreateMap<VentaDTO, Venta>().ForMember(
             destino => destino.Total,
             opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO"))));

            #endregion Venta
            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino => destino.DescriptionProducto,
               opt => opt.MapFrom(origen => Convert.ToString(origen.IdProductoNavigation.Nombre, new CultureInfo("es-CO"))))
                               .ForMember(destino => destino.PrecioTexto, opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
                                .ForMember(destino => destino.TotalTexto, opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))));

            CreateMap<DetalleVentaDTO, DetalleVenta>()
    .ForMember(destino => destino.Precio,
   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-CO"))))
                   .ForMember(destino => destino.Total, opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO"))));

            #endregion DetalleVenta

            #region Reportes
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino => destino.FechaRegistro,
               opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))
                                .ForMember(destino => destino.NumeroDocumento, opt => opt.MapFrom(origen => origen.IdVenta.Value ))
                                .ForMember(destino => destino.TipoPago, opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.TipoPago)))
                                .ForMember(destino => destino.TotalVenta, opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-CO"))))
                                .ForMember(destino => destino.Producto, opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                                .ForMember(destino => destino.Precio, opt => opt.MapFrom(origen => origen.Precio.Value))
                                .ForMember(destino => destino.Total, opt => opt.MapFrom(origen => origen.Total.Value))
;

            #endregion Reportes

        }
    }
}
 