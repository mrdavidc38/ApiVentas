using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.response;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _RolService;

        public RolController(IRolService rolService)
        {
            _RolService = rolService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> lista()
        {
            var rsp = new Response<List<RolDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _RolService.Lista();
                

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
