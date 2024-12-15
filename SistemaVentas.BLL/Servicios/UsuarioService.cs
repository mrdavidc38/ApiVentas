using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SIstemaVentas.Model.Models;
using SsitemaVentas.DAL.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.BLL.Servicios
{
    public class UsuarioService :IUsuarioServicie
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            var queryUsuario = await _usuarioRepositorio.Consultar();
            var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();

            return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(u=>u.Correo==correo 
                && u.Clave == clave);
                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");
                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<UsuarioDTO> crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));

                if (usuarioCreado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }

                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            var usuarioModelo = _mapper.Map<Usuario>(modelo);
            var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);

            if (usuarioEncontrado.IdUsuario == null)
                throw new TaskCanceledException("No exite el usuario");
            usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
            usuarioEncontrado.Correo = usuarioModelo.Correo;
            usuarioEncontrado.IdRol = usuarioModelo.IdRol;
            usuarioEncontrado.Clave = usuarioModelo.Clave;
            usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                
            
            bool usuarioEditado = await _usuarioRepositorio.Editar(usuarioEncontrado);

            if (!usuarioEditado)
                throw new TaskCanceledException("No se pudo editar el usuario");

            return usuarioEditado;
        }

        public async Task<bool> Eliminar(int id)
        {
            var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == id);

            if (usuarioEncontrado == null)
                throw new TaskCanceledException("no existe le usuario");

            var usuarioElininado = await _usuarioRepositorio.Eliminar(usuarioEncontrado);

            if (usuarioElininado)
                throw new TaskCanceledException("usuaro no eliminado");

            return usuarioElininado;
        }


    }
}
