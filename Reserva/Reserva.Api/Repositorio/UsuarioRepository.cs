using Microsoft.EntityFrameworkCore;
using Reserva.Api.Data;
using Reserva.Domain.Domain;
using Reserva.Domain.Interfaces;

namespace Reserva.Api.Repositorio
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context) {
            _context = context;
        }
        public async  Task AddAsync(Usuario entity)
        {
            await _context.Usuarios.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                // Agora você passa o OBJETO para o Remove
                _context.Usuarios.Remove(usuario);

                // Salva a alteração
                await _context.SaveChangesAsync();
            }
        
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {

            var query = _context.Usuarios.AsNoTracking();
            return query.ToList();

        }


        public async   Task<Usuario?> GetByIdAsync(int id)
        {
           
    return await _context.Usuarios.AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Usuario entity)
        {
            _context.Usuarios.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> VerificarEmailCadastradoAsync(string Email)
        {
            return await _context.Usuarios.AnyAsync(x => x.Email == Email);
        }
    }
}
