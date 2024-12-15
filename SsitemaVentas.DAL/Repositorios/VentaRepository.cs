using SIstemaVentas.Model.Models;
using SsitemaVentas.DAL.DBContext;
using SsitemaVentas.DAL.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsitemaVentas.DAL.Repositorios
{
    public class VentaRepository :GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbDatacontext;

        public VentaRepository(DbventaContext context):base(context) 
        { 
         _dbDatacontext = context;
        }

        public async Task<Venta> registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();
            using (var transaction = _dbDatacontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var dv in modelo.DetalleVenta)
                    {
                        var productoEncontrado = _dbDatacontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        productoEncontrado.Stock = productoEncontrado.Stock - dv.Cantidad;
                        _dbDatacontext.Productos.Update(productoEncontrado);
                    }

                    await _dbDatacontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbDatacontext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbDatacontext.NumeroDocumentos.Update(correlativo);
                    await _dbDatacontext.SaveChangesAsync();

                    int cantidadCeros = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0",cantidadCeros));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length-cantidadCeros, cantidadCeros);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbDatacontext.Venta.AddAsync(modelo);
                    await _dbDatacontext.SaveChangesAsync();

                    ventaGenerada = modelo;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    throw;
                }
            }
            return ventaGenerada;
        }
    }

}
