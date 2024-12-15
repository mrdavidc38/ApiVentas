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
    public class ProductoService :IproductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        //public Task<ProductoDTO> crear(ProductoDTO modelo)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> Editar(ProductoDTO modelo)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> Eliminar(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<ProductoDTO>> Lista()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ProductoDTO> crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }

                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);

                var productoEncontrado = await _productoRepositorio.Obtener(u =>
                u.IdProducto == productoModelo.IdProducto
                );

                if (productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;

                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("Nose pudeo editar");

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == id);
                if (productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                bool respuesta = await _productoRepositorio.Eliminar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("Nose pudeo eliminar");

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consultar();
                var queryProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(queryProductos.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
