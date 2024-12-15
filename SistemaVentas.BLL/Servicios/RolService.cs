using AutoMapper;
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
    public class RolService:IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _rolRepositorio = rolRepositorio;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaErrores = await _rolRepositorio.Consultar();
                return _mapper.Map<List<RolDTO>>(listaErrores.ToList());

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
