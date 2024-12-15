﻿using AutoMapper;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SIstemaVentas.Model.Models;
using SsitemaVentas.DAL.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.BLL.Servicios
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public DashBoardService(IVentaRepository ventaRepository, IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        private IQueryable<Venta> retornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            var newDate = ultimaFecha.Value.Date.AddDays(restarCantidadDias);
            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= newDate);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-PE"));
        }

        private async Task<int> TotalProducto()
        {
            IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();
            int total = _productoQuery.Count();

            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();
            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                resultado = tablaVenta.GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(v => v.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }
            return resultado;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO vmDashboard = new DashBoardDTO();
            try
            {
                vmDashboard.TotalVentas = await TotalVentasUltimaSemana();
                vmDashboard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashboard.TotalProductos = await TotalProducto();

                List<VentaSemanaDTO> listaVentasSemana = new List<VentaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listaVentasSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                vmDashboard.VentasUltimaSemana = listaVentasSemana;
            }
            catch (Exception)
            {

                throw;
            }

            return vmDashboard;
        }
    }
}
