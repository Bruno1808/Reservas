using Microsoft.EntityFrameworkCore;
using Reserva.Api.Data;
using Reserva.Domain.Domain;
using Reserva.Domain.Interfaces;

namespace Reserva.Api.Repositorio
{
    public class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly AppDbContext _context;
        public LaboratorioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Laboratorios entity)
        {
            await _context.Laboratorios.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);

            if (laboratorio != null)
            {
                // Agora você passa o OBJETO para o Remove
                _context.Laboratorios.Remove(laboratorio);

                // Salva a alteração
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Laboratorios>> GetAllAsync()
        {
            var query = _context.Laboratorios.AsNoTracking();
            return query.ToList();
        }

        public async Task<Laboratorios> GetByIdAsync(int id)
        {
            return await _context.Laboratorios.AsNoTracking()
                          .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Laboratorios entity)
        {
            _context.Laboratorios.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
