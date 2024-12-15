using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIstemaVentas.Model;
using SIstemaVentas.Model.Models;

namespace SsitemaVentas.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> registrar(Venta modelo);
    }
}
