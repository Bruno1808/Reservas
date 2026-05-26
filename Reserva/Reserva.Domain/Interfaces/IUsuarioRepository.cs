using Reserva.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<bool> VerificarEmailCadastradoAsync(string token);
        Task AddAsync(Usuario entity);
        Task UpdateAsync(Usuario entity);
        Task DeleteAsync(int id);
        
    }
}
