using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.response;
using SistemaVentas.BLL.Servicios;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using System.Globalization;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> registrar([FromBody] VentaDTO venta)
        {

            var rsp = new Response<VentaDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.registrar(venta);


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {

            var rsp = new Response<List<VentaDTO>>();
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }


        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string fechaInicio, string fechaFin)
        {

            var rsp = new Response<List<ReporteDTO>>();

            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.Reporte(fechaInicio, fechaFin);


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }
    }
}
