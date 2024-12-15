using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SsitemaVentas.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SsitemaVentas.DAL.Repositorios.Contrato;
using SsitemaVentas.DAL.Repositorios;
using SistemaVentas.Utility;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(options =>
            {
                var con = configuration.GetConnectionString("cadenaSQL");
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioServicie, UsuarioService>();
            services.AddScoped <ICategoriaService, CategoriaService>();
            services.AddScoped<IproductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
        }

    }
}
