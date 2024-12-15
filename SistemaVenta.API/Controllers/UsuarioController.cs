using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.response;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicie _usuarioService;

        public UsuarioController(IUsuarioServicie usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> lista()
        {

            var rsp = new Response<List<UsuarioDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.Lista();


            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("iniciarSesion")]
        public async Task<IActionResult> iniciarSesion([FromBody] LoginDTO login)
        {

            var rsp = new Response<SesionDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);


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
        public async Task<IActionResult> guardar([FromBody] UsuarioDTO user)
        {

            var rsp = new Response<UsuarioDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.crear(user);


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
        public async Task<IActionResult> editar([FromBody] UsuarioDTO user)
        {

            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.Editar(user);


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
                rsp.value = await _usuarioService.Eliminar(id);


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
