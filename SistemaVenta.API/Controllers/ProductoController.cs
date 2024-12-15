using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.response;
using SistemaVentas.BLL.Servicios;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IproductoService _productoService;

        public ProductoController(IproductoService productoService)
        {
            _productoService = productoService;
        }
        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> lista()
        {

            var rsp = new Response<List<ProductoDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _productoService.Lista();


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }
        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> guardar([FromBody] ProductoDTO producto)
        {

            var rsp = new Response<ProductoDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _productoService.crear(producto);


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> editar([FromBody] ProductoDTO producto)
        {

            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _productoService.Editar(producto);


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }

        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public async Task<IActionResult> eliminar(int id)
        {

            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _productoService.Eliminar(id);


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
