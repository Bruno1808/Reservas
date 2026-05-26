using Microsoft.EntityFrameworkCore;
using Reserva.Api.Data;
using Reserva.Domain.Domain;
using Reserva.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace Reserva.Api.Repositorio
{
    public class ReservasRepository : IReservaRepository
    {
        private readonly AppDbContext _context;
        public ReservasRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task AddAsync(Reservas entity)
        {
            await _context.Reservas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservas = await _context.Reservas.FindAsync(id);

            if (reservas != null)
            {
                // Agora você passa o OBJETO para o Remove
                _context.Reservas.Remove(reservas);

                // Salva a alteração
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Reservas>> GetAllAsync()
        {
            var query = _context.Reservas.AsNoTracking().Include(r => r.Laboratorios).Include(r => r.usuario);
            return query.ToList();
        }

        public async Task<Reservas> GetByIdAsync(int id)
        {
            return await _context.Reservas.AsNoTracking()
                          .FirstOrDefaultAsync(x => x.Id == id);
        }

     
        public async Task<IEnumerable<Reservas>> GetBylaboratorioidAsync(int laboratorioid)
        {
            var query = _context.Reservas.AsNoTracking().Where(x => x.Laboratorios.Id == laboratorioid).ToList();
            return query.ToList();
        }
        public async Task<IEnumerable<Reservas>> GetByusuarioidAsync(int usuarioid)
        {
            var query = _context.Reservas.AsNoTracking().Where(x => x.usuario.Id == usuarioid).ToList();
            return query.ToList();
        }

        public async Task UpdateAsync(Reservas entity)
        {
            _context.Reservas.Update(entity);

            await _context.SaveChangesAsync();
        }

        
        public async Task<bool> ValidarHorarioDisponivelLaboratorio(DateTime? dataInicio, DateTime? dataFinal, int? id)
        {
            try
            {
                // 1. Verificação de nulidade (evita NullReferenceException)
                if (!dataInicio.HasValue || !dataFinal.HasValue || !id.HasValue)
                    return false;

                // 2. Lógica de Sobreposição (Overlap)
                // Existe conflito se: (Nova_Inicio < Existente_Fim) E (Nova_Fim > Existente_Inicio)
                bool existeConflito = await _context.Reservas.AnyAsync(r =>
                    r.LaboratorioId == id &&
                    r.Status == Domain.Enum.Status.CONFIRMADO &&
                    dataInicio < r.DataFinal &&
                    dataFinal > r.DataInicio
                );

                return existeConflito;
            }
            catch (Exception ex)
            {
                // Logar o erro 'ex' aqui se necessário
                return false;
            }
        }


    }
}
