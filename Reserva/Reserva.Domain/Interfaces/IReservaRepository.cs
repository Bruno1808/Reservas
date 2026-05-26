using Reserva.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Interfaces
{
    public interface IReservaRepository
    {

        Task<IEnumerable<Reservas>> GetAllAsync();
        Task<Reservas> GetByIdAsync(int id);
        Task<IEnumerable<Reservas>> GetBylaboratorioidAsync(int laboratorioid);
        Task<IEnumerable<Reservas>> GetByusuarioidAsync(int usuarioid);
        Task AddAsync(Reservas entity);
        Task UpdateAsync(Reservas entity);
        Task DeleteAsync(int id);
        Task<bool> ValidarHorarioDisponivelLaboratorio(DateTime? DataInicio,DateTime? DataFinal, int? laboratorioid);



    }
}
