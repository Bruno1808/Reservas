using Reserva.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Interfaces
{
    public interface ILaboratorioRepository
    {
        
        Task<IEnumerable<Laboratorios>> GetAllAsync();
        Task<Laboratorios> GetByIdAsync(int id);
        Task AddAsync(Laboratorios entity);
        Task UpdateAsync(Laboratorios entity);
        Task DeleteAsync(int id);
    



}
}
