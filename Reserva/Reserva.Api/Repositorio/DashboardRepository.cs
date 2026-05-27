using Microsoft.EntityFrameworkCore;
using Reserva.Api.Data;
using Reserva.Domain.DTO;
using Reserva.Domain.Interfaces;

namespace Reserva.Api.Repositorio
{
    public class DashboardRepository : IDashboard
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDTO> ObterDadosDashboardAsync()
        {
            var dashboard = new DashboardDTO();
            try
            {
                dashboard.QuantidadeUsuarios = await _context.Usuarios.CountAsync();
                dashboard.QuantidadeLaboratorios = await _context.Laboratorios.CountAsync();

                dashboard.Top10Laboratorios = await _context.Reservas
                     .Where(r => r.Laboratorios != null) // Garante que a relação existe
                     .GroupBy(r => new { r.Laboratorios.Nome })
                     .Select(g => new TopLaboratorioDto
                     {

                         Nome = g.Key.Nome,
                         Total = g.Count()
                     })
                     .OrderByDescending(x => x.Total)
                     .Take(10)
                     .ToListAsync();


                dashboard.Top10Usuarios = await _context.Reservas
                    .Where(r => r.usuario != null)
                    .GroupBy(r => new { r.UsuarioId, r.usuario.Nome })
                    .Select(g => new TopUsuarioDto
                    {
                        
                        Nome = g.Key.Nome,
                        Total = g.Count()
                    })
                    .OrderByDescending(x => x.Total)
                    .Take(10)
                    .ToListAsync();

                dashboard.GraficoReservas = await _context.Reservas
    .GroupBy(r => r.Status)
    .Select(g => new DadosGraficoReservaDto
    {
        // Se for Enum, isso garante que pegamos o nome dele, ou tratamos strings vazias
        Label = g.Key.ToString() == "" ? "Não Definido" : g.Key.ToString(),
        Quantidade = g.Count()
    })
    .ToListAsync();
                return dashboard;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
